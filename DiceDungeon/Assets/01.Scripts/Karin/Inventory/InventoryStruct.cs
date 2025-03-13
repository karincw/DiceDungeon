
using UnityEngine;

namespace karin.Inventory
{
    [System.Serializable]
    public struct InvenData
    {
        public ItemNames itemName;
        public Sprite image;
        public int count;
        public int maxCount;

        public InvenData(ItemNames _itemNames, Sprite _sprite, int _count, int _maxCount)
        {
            itemName = _itemNames;
            image = _sprite;
            count = _count;
            maxCount = _maxCount;
        }
    }

}