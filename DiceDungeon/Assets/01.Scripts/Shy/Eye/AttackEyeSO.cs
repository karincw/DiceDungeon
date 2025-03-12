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
        public AttackEffect attackEffect;
        [Header("AttackEffectValues")]
        public BuffEyeSO buffEye;

        public override void OnUse(Agent _agent)
        {
            Debug.Log($"{_agent.gameObject.name} АјАн");
            EventManager.Instance.AttackEvent?.Invoke(GetData(_agent));
        }

        public AttackData GetData(Agent _agent)
        {
            AttackData at = new AttackData();
            at.who = _agent;
            at.where = new Vector2Int(0, 0);
            at.direction = _agent.direction;
            at.attackType = attackType;
            at.damage = value;
            at.effect = attackEffect;
            if (at.effect == AttackEffect.EnchantBuff)
            {
                at.buffData = buffEye.GetData(_agent);
            }
            return at;
        }
    }
}
