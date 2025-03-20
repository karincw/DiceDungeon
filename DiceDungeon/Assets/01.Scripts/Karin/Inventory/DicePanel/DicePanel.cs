using System.Collections.Generic;
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

        public void Close()
        {
            selects[viewIdx].ReWriteDiceData();
        }

        public EyeItemSO MakeNoneItem()
        {
            EyeItemSO so = Instantiate(_itemBase);
            so.count = 1;
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