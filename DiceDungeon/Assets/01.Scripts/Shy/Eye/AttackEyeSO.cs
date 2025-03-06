using Karin;
using Karin.Charactor;
using UnityEngine;

namespace SHY
{
    [CreateAssetMenu(fileName = "AttackEyeSO", menuName = "SO/Eye/Attack")]
    public class AttackEyeSO : EyeSO
    {
        public AttackType attackType;
        public AttackEffect attackEffect;

        public override void OnUse(Agent _agent)
        {
            Debug.Log("АјАн");
            return;

            AttackData at = new AttackData();
            at.who = _agent;
            //where
            at.direction = _agent.direction;
            at.attackType = attackType;
            at.damage = value;
            at.effect = attackEffect;

            InvokeData(at);
        }
    }
}
