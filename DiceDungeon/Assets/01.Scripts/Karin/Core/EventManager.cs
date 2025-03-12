using DG.Tweening;
using System;
using UnityEngine;
using karin.HexMap;
using karin.Charactor;
using karin.BuffSystem;

namespace karin.Event
{

    public class EventManager : MonoSingleton<EventManager>
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

        private void AttackEventHandler(AttackData ad)
        {
            var owner = ad.who;
            var offsetPos = ad.where;
            var damage = ad.damage;

            var strength = owner.buffContainer.GetBuff(Buff.Strength);
            if (strength != null)
            {
                damage = Mathf.RoundToInt(damage * 1.5f);
                owner.buffContainer.RemoveBuff(Buff.Strength);
            }

            var ownerHex = owner.underTile.HexCoords;
            HexTile targetTile = MapManager.Instance.GetTile(ownerHex + offsetPos);

            var attackTargets = targetTile.GetNeighbourData(ad.direction, ad.attackType);
            attackTargets.ForEach(t =>
            {
                t.health.DecreaseHealth(damage);
                switch (ad.effect)
                {
                    case AttackEffect.None:
                        break;
                    case AttackEffect.EnchantBuff:
                        var bd = ad.buffData;
                        bd.who = t;
                        BuffEvent?.Invoke(bd);
                        break;
                }
            });
        }

        private void ShieldEventHandler(ShieldData sd)
        {
            var owner = sd.who;
            owner.health.shield += sd.value;
        }

        private void MoveEventHandler(MoveData md)
        {
            if (md.distance <= 0)
                return;

            var owner = md.who;
            owner.MoveStart(md.direction, md.rewriteTile);
            Vector2 startPos = owner.transform.position;
            Vector2 targetPos = new();
            Action callback = null;

            for (var i = 1; i <= md.distance; i++)
            {
                targetPos = startPos + HexCoordinates.GetDirectionToVector(md.direction) * i;
                HexTile targetTile = MapManager.Instance.GetTile(targetPos);

                if (md.effect == MoveEffect.Collision)
                {
                    if (targetTile == null)
                    {
                        Debug.Log($"{md.who}가 {md.direction}방향으로 {md.distance}만큼 이동할수 없음");
                        if (i == 1) return;

                        targetPos = (Vector2)owner.transform.position + HexCoordinates.GetDirectionToVector(md.direction) * (i - 1);
                        break;
                    }
                    else if (targetTile.moveAble == false)
                    {
                        Debug.Log($"{md.who}가 {md.direction}방향으로 {md.distance}거리에 충돌체를 확인함");

                        var colTarget = targetTile.overAgent;
                        targetPos = (Vector2)owner.transform.position + HexCoordinates.GetDirectionToVector(md.direction) * i;
                        var colPos = (Vector2)owner.transform.position + HexCoordinates.GetDirectionToVector(md.direction) * (i + 1);
                        targetTile = MapManager.Instance.GetTile(colPos);
                        if (targetTile != null)
                        {
                            callback = () =>
                            {
                                MoveData colTargetMD = new MoveData(colTarget, md.direction, MoveEffect.None, 1, 0);
                                MoveEvent?.Invoke(colTargetMD);
                            };
                        }
                        else
                        {
                            callback = () =>
                            {
                                MoveData returnOwnerMD = new MoveData(owner, HexCoordinates.InvertDirection(md.direction), MoveEffect.None, 1, 0, false);
                                MoveEvent?.Invoke(returnOwnerMD);
                            };
                        }
                        colTarget.health.DecreaseHealth(md.additionalValue);
                        break;
                    }
                }
                else
                {

                    if (targetTile == null || targetTile.moveAble == false)
                    {
                        Debug.Log($"{md.who}가 {md.direction}방향으로 {md.distance}만큼 이동할수 없음");
                        if (i == 1) return;

                        targetPos = (Vector2)owner.transform.position + HexCoordinates.GetDirectionToVector(md.direction) * (i - 1);
                        break;
                    }
                }
            }

            MoveAgent(owner, targetPos, md, callback);
        }

        private void MoveAgent(Agent agent, Vector2 destination, MoveData md, Action callbackAction = null)
        {
            agent.transform
                .DOMove(destination, 0.5f).SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    agent.MoveEnd(md.rewriteTile);
                    if (callbackAction != null)
                    {
                        callbackAction?.Invoke();
                    }
                });
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