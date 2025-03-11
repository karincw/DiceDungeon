using DG.Tweening;
using System;
using UnityEngine;
using Karin.HexMap;
using Karin.Charactor;
using Karin.BuffSystem;

namespace Karin.Event
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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                foreach (var item in FindObjectsByType<Agent>(FindObjectsSortMode.None))
                {
                    item.TurnReset();
                }
            }
        }

        private void AttackEventHandler(AttackData ad)
        {
            var owner = ad.who;
            var offsetPos = ad.where;

            var ownerHex = HexCoordinates.ConvertPositionToOffset(owner.transform.position);
            HexTile targetTile = MapManager.Instance.GetTile(ownerHex + offsetPos);

            var attackTargets = targetTile.GetNeighbourData(ad.direction, ad.attackType);
            attackTargets.ForEach(t =>
            {
                t.health.DecreaseHealth(ad.damage);
            });
        }

        private void ShieldEventHandler(ShieldData sd)
        {
            var owner = sd.who;
            owner.health.shield += sd.shield;
        }

        private void MoveEventHandler(MoveData md)
        {
            if (md.distance <= 0)
                return;

            var owner = md.who;
            owner.MoveStart(md.direction);
            var startHex = HexCoordinates.ConvertPositionToOffset(owner.transform.position);
            Vector2 targetPos = new();


            for (var i = 1; i <= md.distance; i++)
            {
                targetPos = (Vector2)owner.transform.position + HexCoordinates.GetDirectionToVector(md.direction) * i;
                HexTile targetTile = MapManager.Instance.GetTile(targetPos);

                if (targetTile == null || targetTile.moveAble == false)
                {
                    Debug.Log($"{md.who}가 {md.direction}방향으로 {md.distance}만큼 이동할수 없음");
                    if (i == 1) return;

                    targetPos = (Vector2)owner.transform.position + HexCoordinates.GetDirectionToVector(md.direction) * (i - 1);
                    break;
                }
            }

            MoveAgent(owner, targetPos);
        }

        private void MoveAgent(Agent agent, Vector2 destination)
        {
            agent.transform
                .DOMove(destination, 0.5f).SetEase(Ease.Linear)
                .OnComplete(() => agent.MoveEnd());
        }

        private void BuffEventHandler(BuffData bd)
        {
            var agent = bd.who;
            var buffContainer = agent.buffContainer;
            var buff = Instantiate(BuffManager.Instance.BuffList[(int)bd.buffType]);
            buff.owner = agent;
            buffContainer.AddBuff(buff, bd.value);
        }

    }

}