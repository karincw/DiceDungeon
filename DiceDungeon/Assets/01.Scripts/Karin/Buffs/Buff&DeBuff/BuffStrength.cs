using UnityEngine;

namespace karin.BuffSystem
{
    [CreateAssetMenu(menuName = "SO/karin/Buff/BuffStrength")]
    public class BuffStrength : BuffSO
    {
        public override void HandleOnValueChanged()
        {
            _value = Mathf.Clamp(_value, 0, 1);
        }
    }
} 