using karin.BuffSystem;
using karin.HexMap;
using System;
using UnityEngine;

namespace karin.Charactor
{
    public abstract class Agent : MonoBehaviour
    {
        public Direction direction
        {
            get => _direction;
            set
            {
                visualController.UpdateViewDirection(value);
                _direction = value;
            }
        }
        private Direction _direction = Direction.Right;
        [HideInInspector] public AgentHealth health;
        [HideInInspector] public BuffContainer buffContainer;
        [HideInInspector] public VisualController visualController;
        [HideInInspector] public AttackEffector attackEffector;
        [SerializeField] private short _startHealth = 50;

        public HexTile underTile
        {
            get => _underTile;
            set
            {
                if (_underTile == value) return;

                onUndertileChanged?.Invoke();
                _underTile = value;
            }
        }
        protected Action onUndertileChanged;
        private HexTile _underTile;

        protected virtual void Awake()
        {
            health = GetComponent<AgentHealth>();
            buffContainer = GetComponent<BuffContainer>();
            visualController = transform.Find("Visual").GetComponent<VisualController>();
            attackEffector = GetComponentInChildren<AttackEffector>();

            transform.position = HexCoordinates.ConvertOffsetToPosition(HexCoordinates.ConvertPositionToOffset(transform.position));
            visualController.Init(this);
            attackEffector.Init(this);
            health.Init(this, _startHealth);
            visualController.UpdateViewDirection(direction);
        }

        protected virtual void Start()
        {
            MoveEnd();
        }

        //다시 내텀이 돌아왔을때 실행
        public virtual void TurnReset()
        {
            health.shield = 0;
            buffContainer.TurnReset();
        }

        public virtual void MoveEnd(bool ReWrite = true)
        {
            visualController.EndAnimation();
            var tile = MapManager.Instance.GetTile(transform.position);
            if (ReWrite)
            {
                tile.overAgent = this;
                tile.moveAble = false;
                underTile = tile;
                return;
            }
        }

        public virtual void MoveStart(Direction dir, bool ReWrite = true)
        {
            direction = dir;
            visualController.UpdateViewDirection(direction);
            visualController.PlayAnimation("Walk");
            if (ReWrite)
            {
                underTile.overAgent = null;
                underTile.moveAble = true;
                return;
            }
        }
    }
}
