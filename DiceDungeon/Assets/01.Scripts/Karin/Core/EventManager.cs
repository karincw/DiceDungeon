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

            int damage = DamageClaculate(ad.damage, owner.buffContainer);

            var ownerHex = HexCoordinates.ConvertPositionToOffset(owner.transform.position);
            HexTile targetTile = MapManager.Instance.GetTile(ownerHex + offsetPos);

            var attackTargets = targetTile.GetNeighbourData(ad.direction, ad.attackType);
            attackTargets.ForEach(t =>
            {
                t.health.DecreaseHealth(damage);
                AttackEffectHandler(ad, ad.effect, t);
            });
        }

        public void DirectAttack(Agent target, AttackData ad)
        {
            var owner = ad.who;
            int damage = DamageClaculate(ad.damage, owner.buffContainer);
            target.health.DecreaseHealth(damage);
            AttackEffectHandler(ad, ad.effect, target);
        }

        private int DamageClaculate(int damage, BuffContainer buffContainer)
        {
            if (buffContainer.Contains(Buff.Strength, out var buffSO))
            {
                damage = Mathf.RoundToInt(damage * 1.5f);
                buffContainer.RemoveBuff(Buff.Strength);
            }

            return damage;
        }

        private void AttackEffectHandler(AttackData ad, AttackEffect af, Agent target)
        {
            switch (af)
            {
                case AttackEffect.EnchantBuff:
                    var bd = ad.buffData;
                    bd.who = target;
                    BuffEvent?.Invoke(bd);
                    break;
            }
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
            Vector2 startPos = owner.transform.position;
            Vector2 targetPos = new();

            Action callback = null;

            if (md.effect != MoveEffect.None)
                targetPos = GetMaxDistanceEffect(owner, md.distance, startPos, md.direction, md.effect, md.additionalValue, out callback);
            else
                targetPos = GetMaxDistance(owner, md.distance, startPos, md.direction);

            MoveAgent(owner, targetPos, md, callback);
        }

        private Vector2 GetMaxDistance(Agent owner, int maxDistance, Vector2 startPos, Direction direction)
        {
            Vector2 targetPos = new();
            for (int i = 0; i < maxDistance; i++)
            {
                targetPos = startPos + HexCoordinates.GetDirectionToVector(direction) * i;
                HexTile targetTile = MapManager.Instance.GetTile(targetPos);

                if (targetTile == null || targetTile.moveAble == false)
                {
                    if (i == 1) break;

                    targetPos = (Vector2)owner.transform.position + HexCoordinates.GetDirectionToVector(direction) * (i - 1);
                    break;
                }
            }
            return targetPos;
        }

        private Vector2 GetMaxDistanceEffect(Agent owner, int maxDistance, Vector2 startPos, Direction direction, MoveEffect mf, int additionalValue, out Action callback)
        {
            Vector2 targetPos = new();
            callback = null;
            for (int i = 0; i < maxDistance; i++)
            {
                targetPos = startPos + HexCoordinates.GetDirectionToVector(direction) * i;
                HexTile targetTile = MapManager.Instance.GetTile(targetPos);

                if (mf == MoveEffect.Collision)
                {
                    if (targetTile == null) //가는 길에 타일이 없어 이동하지 못함
                    {
                        if (i == 1) break;
                        targetPos = (Vector2)owner.transform.position + HexCoordinates.GetDirectionToVector(direction) * (i - 1);
                    }
                    else if (targetTile.moveAble == false) //가는 길에 적이 있어 이동하지 못함
                    {
                        var colTarget = targetTile.overAgent;                                                                       //충돌하는 agent
                        var colPos = (Vector2)owner.transform.position + HexCoordinates.GetDirectionToVector(direction) * (i + 1);  //충돌되어 밀려날위치
                        var colTile = MapManager.Instance.GetTile(colPos);                                                           //출돌되어 밀려날곳의 타일

                        targetPos = (Vector2)owner.transform.position + HexCoordinates.GetDirectionToVector(direction) * i;         //이동할위치

                        if (colTile != null) //밀려날곳이 있음
                        {
                            callback = () =>
                            {
                                MoveData colTargetMD = new MoveData(
                                    colTarget, direction, MoveEffect.None, 1, 0); //타겟을 밀어냄
                                MoveEvent?.Invoke(colTargetMD);
                            };
                        }
                        else                //밀려날곳이 없음
                        {
                            callback = () =>
                            {
                                MoveData returnOwnerMD = new MoveData(
                                    owner, HexCoordinates.InvertDirection(direction), MoveEffect.None, 1, 0, false); //다시 돌아옴
                                MoveEvent?.Invoke(returnOwnerMD);
                            };
                        }
                        DirectAttack(colTarget, new AttackData(owner, Vector2Int.zero, Direction.Left, 1, AttackType.Around, additionalValue, AttackEffect.None));

                    }
                }
            }
            return targetPos;
        }

        private void MoveAgent(Agent agent, Vector2 destination, MoveData md, Action callbackAction = null)
        {
            agent.MoveStart(md.direction, md.rewriteTile);

            agent.transform
                .DOMove(destination, 0.1f).SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    agent.MoveEnd(md.rewriteTile);

                    if (callbackAction != null)
                        callbackAction?.Invoke();
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