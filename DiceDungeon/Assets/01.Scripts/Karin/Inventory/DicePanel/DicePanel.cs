using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace karin.Inventory
{

    public class DicePanel : MonoBehaviour
    {
        public List<DiceSelect> selects;
        private bool interactable = true;
        public int viewIdx;

        private void Start()
        {
            selects[0].SetUpDiagram();
            viewIdx = 0;
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