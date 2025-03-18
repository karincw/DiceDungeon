using karin;
using karin.Charactor;
using karin.Event;
using UnityEngine;

namespace SHY
{
    [CreateAssetMenu(fileName = "BufEyeSO", menuName = "SO/Eye/Buf")]
    public class BuffEyeSO : EyeSO
    {
        public Buff type;

        public override void OnUse(Agent _agent)
        {
            EventManager.Instance.BuffEvent?.Invoke(GetData(_agent));
        }

        public BuffData GetData(Agent _agent)
        {
            BuffData bu = new BuffData();
            bu.who = _agent;
            bu.buffType = type;
            bu.value = value;

            return bu;
        }
    }
}
