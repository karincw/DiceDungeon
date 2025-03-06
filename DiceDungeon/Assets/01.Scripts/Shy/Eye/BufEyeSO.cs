using Karin;
using Karin.Charactor;
using UnityEngine;

namespace SHY
{
    [CreateAssetMenu(fileName = "BufEyeSO", menuName = "SO/Eye/Buf")]
    public class BufEyeSO : EyeSO
    {
        public buffType type;

        public override void OnUse(Agent _agent)
        {
            Debug.Log("น๖วม");
            return;

            BuffData bu = new BuffData();
            bu.who = _agent;
            bu.buffType = type;
            bu.time = value;

            InvokeData(bu);
        }
    }
}
