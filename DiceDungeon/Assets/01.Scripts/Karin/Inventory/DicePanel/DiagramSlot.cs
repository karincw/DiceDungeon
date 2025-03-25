using UnityEngine;

namespace karin.Inventory
{
    public class DiagramSlot : SlotUI, IShowInfoAble
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
            return resource as EyeItemSO;
        }

        public bool TryGetInfoData(out ShowInfoData data)
        {
            data = new();
            if (resource == null) return false;
            data.infoName = GetResource().itemName.ToString();
            data.infoDescription = GetResource().eye.eyeDescrption;
            return true;
        }
    }
}