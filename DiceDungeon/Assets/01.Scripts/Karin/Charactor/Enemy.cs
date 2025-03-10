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
        private AttackData _selectedAttackData;
        private Pathfinder _pathfinder;

        protected virtual void Awake()
        {
            _pathfinder = new Pathfinder();
        }

        private void Update()
        {

        }

        public virtual void TurnAction()
        {
            if (MoveOnAttackablePos(_eData.maxMoveCount))
            {
                PlayAttack();
            }
        }

        protected virtual void PlayAttack()
        {

        }

        protected bool MoveOnAttackablePos(int maxMoveCount)
        {
            //���߿� �Ŵ������� �������°ɷ� ��ü
            Agent player = FindFirstObjectByType<Player>();
            var attackEyes = _eData.useAbleAbilitys.Where(d => d is AttackEyeSO).Select(d => d as AttackEyeSO).ToList();
            //_selectedAttackData = attackEyes[Random.Range(0, _eData.useAbleAbilitys.Count)];
            //���߿� AttackData���ִ� �����ǥ�� �������� ��ü
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