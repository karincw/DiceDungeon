using SHY;
using UnityEngine;

namespace karin.Inventory
{
    [CreateAssetMenu(menuName = "SO/karin/Inventory/DiagramItemSO")]
    public class EyeItemSO : ItemSO
    {
        [Multiline] public string description;
        public EyeSO eye;
    }

}