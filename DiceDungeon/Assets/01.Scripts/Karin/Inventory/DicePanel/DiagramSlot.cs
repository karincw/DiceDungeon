using UnityEngine;

namespace karin.Inventory
{
    public class DiagramSlot : SlotUI
    {
        private void Start()
        {
            maxCount = 1;
        }

        public override bool CanAdd(int count)
        {
            if (resource is EyeItemSO)
            {
                return base.CanAdd(count);
            }
            return false;
        }

        public EyeItemSO GetResource()
        {
            Debug.Log(resource as EyeItemSO);
            return resource as EyeItemSO;
        }
    }
}