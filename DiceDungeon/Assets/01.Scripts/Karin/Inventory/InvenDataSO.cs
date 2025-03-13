using System.Collections.Generic;
using UnityEngine;

namespace karin.Inventory
{

    [CreateAssetMenu(menuName = "SO/karin/Inventory/InvenDataSO")]
    public class InvenDataSO : ScriptableObject
    {
        public List<InvenData> list = new();
    }


}
