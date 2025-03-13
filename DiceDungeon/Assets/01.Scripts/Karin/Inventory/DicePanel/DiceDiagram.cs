using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace karin.Inventory
{
    public class DiceDiagram : MonoBehaviour
    {
        [SerializeField] private List<SlotUI> _parts;

        public bool IsVaild() => _parts.Select(p => p.HasItem).ToList().Count == 6;

        public List<EyeItemSO> GetData()
        {
            return null;
        }
    }
}