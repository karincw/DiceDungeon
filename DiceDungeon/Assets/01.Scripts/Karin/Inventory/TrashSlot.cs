using UnityEngine;

namespace karin.Inventory
{
    public class TrashSlot : SlotUI
    {
        public override bool CanAdd(int count)
        {
            return true;
        }

        public override int MaxAdd()
        {
            return 999;
        }

        public override void Refresh() 
        {
            resource = null;
        }
    }
}