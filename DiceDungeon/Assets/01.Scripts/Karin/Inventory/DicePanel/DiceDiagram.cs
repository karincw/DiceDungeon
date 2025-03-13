using SHY;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace karin.Inventory
{
    public class DiceDiagram : MonoBehaviour
    {
        [SerializeField] private List<DiagramSlot> _parts;

        public bool IsValidate() => _parts.Select(p => p.HasItem).Where(v => v == true).ToList().Count == 6;

        public List<EyeItemSO> GetData()
        {
            return _parts.Select(s => s.GetResource()).ToList();
        }

        public void SetData(DiceSO dice)
        {
            for (int i = 0; i < _parts.Count; i++)
            {
                ItemSO so = Inventory.Instance.MakeNewItem(dice.eyes[i].itemName, dice.eyes[i].icon);
                _parts[i].resource = so;
                _parts[i].Refresh();
            }
        }
    }
}