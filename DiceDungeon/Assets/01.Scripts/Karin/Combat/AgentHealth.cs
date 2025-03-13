using UnityEngine;

namespace karin.Charactor
{
    public class AgentHealth : MonoBehaviour
    {
        [SerializeField] private int maxHealth;
        [SerializeField] private Gauge _hpGauge;
        private int currentHealth;
        public int shield;

        private void Awake()
        {
            currentHealth = maxHealth;
            _hpGauge.StartGauge1 = maxHealth;
            _hpGauge.currentGauge1 = maxHealth;
        }

        public void IncreaseHealth(int value)
        {
            currentHealth = Mathf.Clamp(currentHealth + value, 0, maxHealth);
            _hpGauge.GaugeIncrease(value);
        }
        public void DecreaseHealth(int value)
        {
            if (shield > 0)
            {
                shield -= value;
                value -= shield < value ? shield : value;
            }

            currentHealth = Mathf.Clamp(currentHealth - value, 0, maxHealth);
            if (currentHealth <= 0)
            {
                Die();
            }
            _hpGauge.GaugeDecrease(value);
        }

        private void Die()
        {

        }
    }
}