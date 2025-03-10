using Karin.HexMap;
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

        public virtual void TurnAction()
        {

        }
    }
}