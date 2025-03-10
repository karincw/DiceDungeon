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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                BuffData bd = new BuffData();
                bd.who = this;
                bd.buffType = Buff.Poison;
                bd.value = 10;
                EventManager.Instance.BuffEvent?.Invoke(bd);
            }
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