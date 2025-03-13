using SHY;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace karin.Inventory
{

    public class DicePanel : MonoBehaviour
    {
        public List<DiceSelect> selects;
        private bool interactable = true;
        [SerializeField] private EyeItemSO _itemBase;
        public int viewIdx;

        private void Start()
        {
            foreach (DiceSelect select in selects)
            {
                select.ButtonAddListener(() =>
                {
                    selects[viewIdx].ReWriteDiceData();
                    viewIdx = select._diceIdx;
                    selects[viewIdx].SetUpDiagram();
                });
            }
        }

        private void Update()
        {
            if (IsAllValidate())
            {
                interactable = true;
                selects.ForEach(s => s.SetInteractable(interactable));
            }
            else
            {
                interactable = false;
                selects.ForEach(s => s.SetInteractable(interactable));
            }

        }

        public void OpenSetUp()
        {
            viewIdx = 0;
            selects[viewIdx].SetUpDiagram();
        }

        public EyeItemSO MakeNewItem(ItemNames name, Sprite image, EyeSO eye)
        {
            EyeItemSO so = Instantiate(_itemBase);
            so.itemName = name;
            so.image = image;
            so.count = 1;
            so.eye = eye;
            return so;
        }

        private bool IsAllValidate()
        {
            foreach (DiceSelect select in selects)
            {
                if (!select.IsValidate())
                {
                    return false;
                }
            }
            return true;
        }

    }

}