using DG.Tweening;
using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Karin
{

    public class EventManager : SingleTon<EventManager>
    {
        public Action<AttackData> AttackEvent;
        public Action<ShieldData> ShieldEvent;
        public Action<MoveData> MoveEvent;
        public Action<BuffData> BuffEvent;

        private void Awake()
        {
            AttackEvent += AttackEventHandler;
            ShieldEvent += ShieldEventHandler;
            MoveEvent += MoveEventHandler;
            BuffEvent += BuffEventHandler;
        }

        private void OnDestroy()
        {
            AttackEvent -= AttackEventHandler;
            ShieldEvent -= ShieldEventHandler;
            MoveEvent -= MoveEventHandler;
            BuffEvent -= BuffEventHandler;
        }

        private void AttackEventHandler(AttackData data)
        {
            throw new NotImplementedException();
        }

        private void ShieldEventHandler(ShieldData data)
        {
            throw new NotImplementedException();
        }

        private void MoveEventHandler(MoveData md)
        {
            if (md.distance <= 0)
                return;

            var target = md.who;
            target.MoveStart();
            var startHex = HexCoordinates.ConvertPositionToOffset(target.transform.position);
            Vector2Int targetHex = new();


            for (var i = 1; i <= md.distance; i++)
            {
                targetHex = startHex + HexCoordinates.GetDirection(md.direction) * i;
                HexTile targetTile = MapManager.Instance.GetTile(targetHex);
                if (targetTile != null || !targetTile.moveAble)
                {
                    Debug.Log($"{md.who}가 {md.direction}방향으로 {md.distance}만큼 이동할수 없음");
                    if (i == 1) return;

                    targetHex = startHex + HexCoordinates.GetDirection(md.direction) * (i - 1);
                }
            }

            MoveAgent(target, HexCoordinates.ConvertOffsetToPosition(targetHex));

        }

        private void MoveAgent(Agent agent, Vector2 destination)
        {
            agent.transform 
                .DOMove(destination, 0.5f).SetEase(Ease.Linear)
                .OnComplete(() => agent.MoveEnd());
        }

        private void BuffEventHandler(BuffData data)
        {
            throw new NotImplementedException();
        }

    }

}