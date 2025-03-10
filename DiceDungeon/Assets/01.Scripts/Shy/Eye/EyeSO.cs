using UnityEngine;
using Karin.Charactor;
using Karin;
using Karin.Event;

namespace SHY
{
    public abstract class EyeSO : ScriptableObject
    {
        public string eyeName;
        public Sprite icon;
        public int value;

        public abstract void OnUse(Agent _agent);
    }
}

