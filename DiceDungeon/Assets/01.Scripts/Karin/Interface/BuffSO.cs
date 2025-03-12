using Karin.Charactor;
using System;
using UnityEngine;

namespace Karin.BuffSystem
{
    public abstract class BuffSO : ScriptableObject
    {
        public Agent owner;
        public int value
        {
            get
            {
                return _value;
            }
            set
            {
                if (value == _value)
                    return;

                _value = value;
                OnValueChanged?.Invoke();
            }
        }
        protected int _value;
        protected Action OnValueChanged;
        public Sprite icon;
        public Buff buffType;

        [SerializeField] protected bool _usePercent;
        [SerializeField] protected int decreaseValue = 1;
        [Tooltip("100% ~ 1% == 1 ~ 0.01")]
        [SerializeField] protected float decreasePercent = 1;

        public virtual void TurnEndSetting()
        {
            if (value == -1) return;

            if (_usePercent)
                value -= Mathf.CeilToInt(value * decreasePercent);
            else
                value -= decreaseValue;

            value = Mathf.Clamp(value, 0, 100000);
        }

        public virtual void HandleOnValueChanged()
        {

        }

        public BuffSO()
        {
            OnValueChanged += HandleOnValueChanged;
        }

        ~BuffSO()
        {
            OnValueChanged -= HandleOnValueChanged;
        }
    }
}