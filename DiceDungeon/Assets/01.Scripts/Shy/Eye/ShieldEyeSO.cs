using Karin;
using Karin.Charactor;
using Karin.Event;
using UnityEngine;

namespace SHY
{
    [CreateAssetMenu(fileName = "ShieldEyeSO", menuName = "SO/Eye/Shield")]
    public class ShieldEyeSO : EyeSO
    {
        public override void OnUse(Agent _agent)
        {
            Debug.Log("¹æ¾î");

            

            EventManager.Instance.ShieldEvent?.Invoke(GetData(_agent));
        }

        public ShieldData GetData(Agent _agent)
        {
            ShieldData sh = new ShieldData();
            sh.who = _agent;
            sh.shield = value;

            return sh;
        }
    }
}
