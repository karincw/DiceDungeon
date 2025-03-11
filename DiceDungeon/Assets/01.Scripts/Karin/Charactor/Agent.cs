using UnityEngine;
using Karin.HexMap;
using Karin.BuffSystem;
using Karin.Event;
using UnityEditor.U2D.Aseprite;

namespace Karin.Charactor
{
    public abstract class Agent : MonoBehaviour
    {
        public Direction direction;
        [HideInInspector] public AgentHealth health;
        [HideInInspector] public BuffContainer buffContainer;
        [HideInInspector] public HexTile underTile;

        protected virtual void Awake()
        {
            health = GetComponent<AgentHealth>();
            buffContainer = GetComponent<BuffContainer>();
            transform.position = HexCoordinates.ConvertOffsetToPosition(HexCoordinates.ConvertPositionToOffset(transform.position));
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

        public virtual void MoveEnd()
        {
            var tile = MapManager.Instance.GetTile(transform.position);
            tile.overAgent = this;
            tile.moveAble = false;
            underTile = tile;
        }

        public virtual void MoveStart(Direction dir)
        {
            direction = dir;
            underTile.overAgent = null;
            underTile.moveAble = true;
        }
    }
}