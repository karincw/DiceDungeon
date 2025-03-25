using SHY;
using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace karin.Inventory
{

    public class DiceSelect : MonoBehaviour
    {
        private Button _targetBtn;
        [Range(0, 4)] public int _diceIdx;
        private DiceSO _currentDice;
        [SerializeField] private DiceDiagram _diagram;

        private void Awake()
        {
            _targetBtn = GetComponent<Button>();
        }
        private void Start()
        {
            _currentDice = GameManager.Instance.playerData.dices[_diceIdx];
        }
        private void OnDestroy()
        {
            _targetBtn.onClick.RemoveAllListeners();
        }

        public void SetInteractable(bool state)
        {
            _targetBtn.interactable = state;
        }
        public void ButtonAddListener(UnityAction action)
        {
            _targetBtn.onClick.AddListener(action);
        }

        public void SetUpDiagram()
        {
            _diagram.SetData(_currentDice);
        }

        public bool IsValidate()
        {
            return _diagram.IsValidate();
        }

        public void ReWriteDiceData()
        {
            _currentDice.eyes = _diagram.GetData().Select(s => s.eye).ToList();
        }
    }

}
