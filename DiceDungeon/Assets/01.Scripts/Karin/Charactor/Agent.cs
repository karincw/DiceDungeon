using UnityEngine;
using Karin.HexMap;

namespace Karin.Charactor
{
    public abstract class Agent : MonoBehaviour
    {
        public Direction direction;
        [HideInInspector] public AgentHealth health;

        private void Awake()
        {
            health = GetComponent<AgentHealth>();
            transform.position = HexCoordinates.ConvertOffsetToPosition(HexCoordinates.ConvertPositionToOffset(transform.position));
        }

        private void Start()
        {
            MoveEnd();
        }


        //다시 내텀이 돌아왔을때 실행
        public virtual void TurnReset()
        {
            health.shield = 0;
        }

        public virtual void MoveEnd()
        {
            var nowTile = MapManager.Instance.GetTile(transform.position);
            nowTile.overAgent = this;
            nowTile.moveAble = false;
        }

        public virtual void MoveStart(Direction dir)
        {
            direction = dir;
            var nowTile = MapManager.Instance.GetTile(transform.position);
            nowTile.overAgent = null;
            nowTile.moveAble = true;
        }
    }
}