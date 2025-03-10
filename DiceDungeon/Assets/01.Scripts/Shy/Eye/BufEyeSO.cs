using Karin;
using Karin.Charactor;
using Karin.Event;
using UnityEngine;

namespace SHY
{
    [CreateAssetMenu(fileName = "BufEyeSO", menuName = "SO/Eye/Buf")]
    public class BufEyeSO : EyeSO
    {
        public Buff type;

        public override void OnUse(Agent _agent)
        {
            Debug.Log("น๖วม");
            return;

            BuffData bu = new BuffData();
            bu.who = _agent;
            bu.buffType = type;
            bu.value = value;

            EventManager.Instance.BuffEvent?.Invoke(bu);
        }
    }
}
