using karin;
using karin.Charactor;
using karin.Event;
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
            EventManager.Instance.MoveEvent?.Invoke(GetData(_agent));
        }

        public MoveData GetData(Agent _agent)
        {
            MoveData mo = new MoveData();
            mo.who = _agent;
            mo.direction = dir;
            mo.distance = value;
            mo.effect = moveEffect;
            mo.additionalValue = CollisionDamage;
            mo.rewriteTile = true;
            return mo;
        }
    }
}
