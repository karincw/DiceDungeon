using DG.Tweening;
using Karin.HexMap;
using SHY;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Karin.Charactor
{

    public class Enemy : Agent
    {
        [SerializeField] private EnemyDataSO _eData;
        private AttackEyeSO _selectedAttack;
        private Pathfinder _pathfinder;

        protected virtual void Awake()
        {
            _pathfinder = new Pathfinder();
        }

        protected virtual void ReservationAttack()
        {
            //나중에 매니저에서 가져오는걸로 교체
            Agent player = FindFirstObjectByType<Player>();
            Direction dir = HexCoordinates.GetVectorToDirection(player.transform.position - transform.position);
            direction = dir;
        }

        protected virtual void PlayAttack()
        {
            _selectedAttack.OnUse(this);
        }

        public virtual void PlayMove()
        {
            if (MoveOnAttackablePos(_eData.maxMoveCount))
            {
                ReservationAttack();
            }
        }

        protected bool MoveOnAttackablePos(int maxMoveCount)
        {
            //나중에 매니저에서 가져오는걸로 교체
            Agent player = FindFirstObjectByType<Player>();

            var attackEyes = _eData.useAbleAbilitys.Where(d => d is AttackEyeSO).Select(d => d as AttackEyeSO).ToList();
            _selectedAttack = attackEyes[Random.Range(0, attackEyes.Count)];
            
            List<HexTile> attackAbleTiles = player.underTile.GetNeighbourTiles;
            var route = _pathfinder.PathFind(underTile, attackAbleTiles);

            Mover(route, 1, maxMoveCount);
            return route.Count <= maxMoveCount;
        }

        protected virtual void Mover(List<Vector2> route, int idx, int max, float duration = 0.1f)
        {
            if (route.Count < idx + 1)
            {
                return;
            }
            MoveStart(HexCoordinates.GetVectorToDirection(route[idx] - route[idx - 1]));
            transform.DOMove(route[idx], duration).OnComplete(() =>
            {
                MoveEnd();
                if (max > idx)
                {
                    idx++;
                    Mover(route, idx, max);
                }
            });

        }

    }
}