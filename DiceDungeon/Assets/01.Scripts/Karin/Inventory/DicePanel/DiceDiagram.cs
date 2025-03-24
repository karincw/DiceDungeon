using SHY;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace karin.Inventory
{
    public class DiceDiagram : MonoBehaviour
    {
        [SerializeField] private List<DiagramSlot> _parts;
        [SerializeField] private DicePanel _dicePanel;
        [SerializeField] private TMP_Text _diceName;
        [SerializeField] private TMP_Text _diceDescription;

        public bool IsValidate() => _parts.Select(p => p.HasItem).Where(v => v == true).ToList().Count == 6;

        public List<EyeItemSO> GetData()
        {
            var result = new List<EyeItemSO>();
            for (int i = 0; i < _parts.Count; i++)
            { 
                if (_parts[i].resource == null)
                    result.Add(_dicePanel.MakeNoneItem());
                else
                    result.Add(_parts[i].GetResource());
            }
            return result;
        }

        public void SetData(DiceSO dice)
        {
            for (int i = 0; i < _parts.Count; i++)
            {
                var currentEye = dice.eyes[i];
                EyeItemSO so = _dicePanel.MakeNoneItem();
                so.itemName = currentEye.itemName;
                so.image = currentEye.icon;
                so.eye = currentEye;
                _parts[i].resource = so;
                _parts[i].Refresh();
            }
            _diceName.text = dice.diceName;
            _diceDescription.text = dice.description;
        }
    }
}