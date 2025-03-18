using karin;
using karin.Charactor;
using UnityEngine;
using karin.Event;

namespace SHY
{
    [CreateAssetMenu(fileName = "AttackEyeSO", menuName = "SO/Eye/Attack")]
    public class AttackEyeSO : EyeSO
    {
        public RangeSO range;
        public AttackType attackType;
        [Range(1, 3)] public int rangeDistance = 1;
        public AttackEffect attackEffect;
        [Header("AttackEffectValues")]
        public BuffEyeSO buffEye;

        public override void OnUse(Agent _agent)
        {
            EventManager.Instance.AttackEvent?.Invoke(GetData(_agent));
        }

        public AttackData GetData(Agent _agent)
        {
            AttackData at = new AttackData(_agent, new Vector2Int(0, 0), _agent.direction, rangeDistance, attackType, value, attackEffect);

            if (attackEffect == AttackEffect.EnchantBuff)
            {
                at.buffData = buffEye.GetData(_agent);
            }

            return at;
        }
    }
}
