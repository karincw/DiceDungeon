using DG.Tweening;
using Karin.HexMap;
using System.Collections.Generic;
using UnityEngine;

namespace Karin.Charactor
{

    public class Enemy : Agent
    {
        [SerializeField] private EnemyDataSO _eData;
        private Pathfinder _pathfinder;

        protected virtual void Awake()
        {
            _pathfinder = new Pathfinder();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F10))
            {
                MoveOnAttackablePos(_eData.maxMoveCount);
            }
        }

        public virtual void TurnAction()
        {

        }

        protected void MoveOnAttackablePos(int maxMoveCount)
        {
            Agent player = FindFirstObjectByType<Player>();
            List<HexTile> attackAbleTiles = player.underTile.GetNeighbourTiles;
            var route = _pathfinder.PathFind(underTile, attackAbleTiles);

            Mover(route, 1, maxMoveCount);
        }

        protected virtual void Mover(List<Vector2> route, int idx, int max, float duration = 0.1f)
        {
            if(route.Count < idx + 1)
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