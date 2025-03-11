using DG.Tweening;
using Karin.HexMap;
using SHY;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Karin.Charactor
{

    public class Enemy : Agent
    {
        [SerializeField] private EnemyDataSO _eData;
        private AttackEyeSO _selectedAttack;
        private Pathfinder _pathfinder;

        protected override void Awake()
        {
            base.Awake();
            _pathfinder = new Pathfinder();
        }

        protected virtual void ReservationAttack(bool playReservation)
        {
            if (playReservation == false) return;
            //나중에 매니저에서 가져오는걸로 교체
            Agent player = FindFirstObjectByType<Player>();
            //Agent player = BattleManager.Instance.player;
            Direction dir = HexCoordinates.GetVectorToDirection(player.transform.position - transform.position);
            direction = dir;
        }

        protected virtual void PlayAttack()
        {
            _selectedAttack.OnUse(this);
        }

        public virtual void PlayMove()
        {
            MoveOnAttackablePos(_eData.maxMoveCount);
        }

        protected void MoveOnAttackablePos(int maxMoveCount)
        {
            //나중에 매니저에서 가져오는걸로 교체
            Agent player = FindFirstObjectByType<Player>();
            //Agent player = BattleManager.Instance.player;

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