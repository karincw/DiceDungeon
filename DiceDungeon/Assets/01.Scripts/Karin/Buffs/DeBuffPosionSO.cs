using UnityEngine;

namespace Karin.BuffSystem
{
    [CreateAssetMenu(menuName = "SO/Karin/Buff/DeBuffPoison")]
    public class DeBuffPosionSO : BuffSO
    {
        [SerializeField] private bool _usePercent;
        [SerializeField] private int decreaseValue = 1;
        [SerializeField] private float decreasePercent = 1;

        public override void TurnEndSetting()
        {
            owner.health.DecreaseHealth(value);
            if (value == -1) return;

            if (_usePercent)
                value = Mathf.CeilToInt(value * decreasePercent);
            else
                value -= decreaseValue;
        }
    }
}