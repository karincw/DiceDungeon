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

        public override void OnUse(Agent _agent)
        {
            Debug.Log("¿Ãµø");

            MoveData mo = new MoveData();
            mo.who = _agent;
            mo.direction = dir;
            mo.distance = value;
            mo.effect = moveEffect;

            EventManager.Instance.MoveEvent?.Invoke(mo);
        }
    }
}
