using Karin.BuffSystem;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Karin/Buff/BuffStrength")]
public class BuffStrength : BuffSO
{
    public override void HandleOnValueChanged()
    {
        _value = Mathf.Clamp(_value, 0, 1);
    }
}
