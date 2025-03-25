using SHY;
using System;
using UnityEngine;

namespace karin.Charactor
{
    public class AgentHealth : MonoBehaviour
    {
        [SerializeField] private Gauge _hpGauge;
        public int shield;
        private int _maxHealth;
        private int currentHealth;
        private Agent _owner;

        public void Init(Agent owner, int maxHealth)
        {
            _owner = owner;
            _maxHealth = maxHealth;
            currentHealth = _maxHealth;
            _hpGauge.StartGauge1 = _maxHealth;
            _hpGauge.currentGauge1 = _maxHealth;
        }

        public void IncreaseHealth(int value)
        {
            currentHealth = Mathf.Clamp(currentHealth + value, 0, _maxHealth);
            _hpGauge.GaugeIncrease(value);
        }
        public void DecreaseHealth(int value)
        {
            if (shield > 0)
            {
                shield -= value;
                value -= shield < value ? shield : value;
            }

            currentHealth = Mathf.Clamp(currentHealth - value, 0, _maxHealth);
            if (currentHealth <= 0)
            {
                Die();
            }
            _hpGauge.GaugeDecrease(value);
        }

        private void Die()
        {
            BattleManager.Instance.dieEvent?.Invoke(_owner);
        }
    }
}