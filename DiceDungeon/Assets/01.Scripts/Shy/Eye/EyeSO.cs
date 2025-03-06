using UnityEngine;
using Karin.Charactor;
using Karin;
using Karin.Event;

namespace SHY
{
    public abstract class EyeSO : ScriptableObject
    {
        public string eyeName;
        public Sprite img;
        public int value;

        public abstract void OnUse(Agent _agent);

        protected void InvokeData(AttackData _data) => EventManager.Instance.AttackEvent?.Invoke(_data);
        protected void InvokeData(MoveData _data) => EventManager.Instance.MoveEvent?.Invoke(_data);
        protected void InvokeData(ShieldData _data) => EventManager.Instance.ShieldEvent?.Invoke(_data);
        protected void InvokeData(BuffData _data) => EventManager.Instance.BuffEvent?.Invoke(_data);
    }
}

