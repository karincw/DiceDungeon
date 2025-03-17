using karin;
using karin.Charactor;
using karin.Event;
using UnityEngine;

namespace SHY
{
    [CreateAssetMenu(fileName = "ShieldEyeSO", menuName = "SO/Eye/Shield")]
    public class ShieldEyeSO : EyeSO
    {
        public override void OnUse(Agent _agent)
        {
            EventManager.Instance.ShieldEvent?.Invoke(GetData(_agent));
        }

        public ShieldData GetData(Agent _agent)
        {
            ShieldData sh = new ShieldData();
            sh.who = _agent;
            sh.value = value;

            return sh;
        }
    }
}
