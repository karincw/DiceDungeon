using DG.Tweening;
using karin.HexMap;
using SHY;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace karin.Charactor
{

    public class Enemy : Agent
    {
        [SerializeField] private EnemyDataSO _eData;
        private AttackEyeSO _selectedAttack;
        private Pathfinder _pathfinder;
        private List<HexTile> _warningtiles;
        public bool isReservation = false;

        protected override void Awake()
        {
            base.Awake();
            _pathfinder = new Pathfinder();
            _warningtiles = new List<HexTile>();
            onUndertileChanged += HandleUnderTileChanged;
        }

        private void OnDestroy()
        {
            onUndertileChanged -= HandleUnderTileChanged;
        }


        private void HandleUnderTileChanged()
        {
            if (isReservation)
            {
                _warningtiles.ForEach(tile => tile.warning = false);
                SetWarningTile(direction, _selectedAttack.attackType);
            }
        }

        protected void SetWarningTile(Direction dir, AttackType type)
        {
            var ad = _selectedAttack.GetData(this);
            var myHex = HexCoordinates.ConvertPositionToOffset(transform.position);
            HexTile targetTile = MapManager.Instance.GetTile(myHex + ad.where);

            var warningTiles = targetTile.GetNeighbourTiles(ad.direction, ad.attackType);
            _warningtiles = warningTiles;
            _warningtiles.ForEach(tile => tile.warning = true);
        }

        protected virtual void ReservationAttack(bool playReservation)
        {
            if (playReservation == false) return;
            Agent player = BattleManager.Instance.player;

            Direction dir = HexCoordinates.GetVectorToDirection(player.transform.position - transform.position);
            direction = dir;

            SetWarningTile(direction, _selectedAttack.attackType);
            isReservation = true;
        }

        public virtual void PlayAttack()
        {
            isReservation = false;
            _selectedAttack.OnUse(this);
            _warningtiles.ForEach(tile => tile.warning = false);
        }

        public virtual void PlayMove()
        {
            MoveOnAttackablePos(_eData.maxMoveCount);
        }

        protected void MoveOnAttackablePos(int maxMoveCount)
        {
            Agent player = BattleManager.Instance.player;

            var attackEyes = _eData.useAbleAbilitys.Where(d => d is AttackEyeSO).Select(d => d as AttackEyeSO).ToList();
            _selectedAttack = attackEyes[Random.Range(0, attackEyes.Count)];

            List<HexTile> attackAbleTiles = _selectedAttack.range.Get()
                .Select(t => MapManager.Instance.GetTile(
                    HexCoordinates.ConvertPositionToOffset(player.transform.position) + new Vector2Int((int)t.x, (int)t.y)))
                .Where(t => t is not null)
                .ToList();
            var route = _pathfinder.PathFind(underTile, attackAbleTiles);

            Mover(route, 1, maxMoveCount, play => ReservationAttack(play));
        }

        protected virtual void Mover(List<Vector2> route, int idx, int max, Action<bool> callbackAction = null, float duration = 0.1f)
        {
            if (route.Count < idx + 1)
            {
                callbackAction?.Invoke(route.Count <= max);
                return;
            }
            MoveStart(HexCoordinates.GetVectorToDirection(route[idx] - route[idx - 1]));
            transform.DOMove(route[idx], duration).OnComplete(() =>
            {
                MoveEnd();
                if (max > idx)
                {
                    idx++;
                    Mover(route, idx, max, callbackAction);
                }
            });
        }

    }
}