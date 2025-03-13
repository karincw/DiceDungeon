using karin.Charactor;
using karin.Inventory;
using UnityEngine;

namespace SHY
{
    public abstract class EyeSO : ScriptableObject
    {
        public string eyeName;
        public Sprite icon;
        public int value;
        public ItemNames itemName;

        public abstract void OnUse(Agent _agent);
    }
}

