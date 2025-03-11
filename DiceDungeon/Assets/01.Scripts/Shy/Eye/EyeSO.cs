using Karin.Charactor;
using UnityEngine;

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

