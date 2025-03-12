using Karin;
using Karin.Charactor;
using Karin.Event;
using UnityEngine;

namespace SHY
{
    [CreateAssetMenu(fileName = "AttackEyeSO", menuName = "SO/Eye/Move")]
    public class MoveEyeSO : EyeSO
    {
        public Direction dir;
        public MoveEffect moveEffect;
        [Header("moveEffectValues")]
        public int CollisionDamage;

        public override void OnUse(Agent _agent)
        {
            Debug.Log("¿Ãµø");

            MoveData mo = new MoveData();
            mo.who = _agent;
            mo.direction = dir;
            mo.distance = value;
            mo.effect = moveEffect;
            mo.additionalValue = CollisionDamage;

            EventManager.Instance.MoveEvent?.Invoke(mo);
        }

        public MoveData GetData(Agent _agent)
        {
            MoveData mo = new MoveData();
            mo.who = _agent;
            mo.direction = dir;
            mo.distance = value;
            mo.effect = moveEffect;
            mo.additionalValue = CollisionDamage;
            return mo;
        }
    }
}
