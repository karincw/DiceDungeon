using Karin;
using Karin.Charactor;
using UnityEngine;

namespace SHY
{
    [CreateAssetMenu(fileName = "ShieldEyeSO", menuName = "SO/Eye/Shield")]
    public class ShieldEyeSO : EyeSO
    {
        public override void OnUse(Agent _agent)
        {
            Debug.Log("���");
            return;

            ShieldData sh = new ShieldData();
            sh.who = _agent;
            sh.shield = value;

            InvokeData(sh);
        }
    }
}
