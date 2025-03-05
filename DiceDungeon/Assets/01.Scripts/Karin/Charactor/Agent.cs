using UnityEngine;
using Karin.HexMap;
using Karin.Buff;

namespace Karin.Charactor
{
    public abstract class Agent : MonoBehaviour
    {
        public Direction direction;
        [HideInInspector] public AgentHealth health;
        [HideInInspector] public BuffContainer buffContainer;

        private void Awake()
        {
            health = GetComponent<AgentHealth>();
            buffContainer = GetComponent<BuffContainer>();
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
            buffContainer.TurnReset();
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