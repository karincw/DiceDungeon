using UnityEngine;

namespace karin.BuffSystem
{
    [CreateAssetMenu(menuName = "SO/karin/Buff/DeBuffPoison")]
    public class DeBuffPosionSO : BuffSO
    {
        public override void TurnEndSetting()
        {
            owner.health.DecreaseHealth(value);
            base.TurnEndSetting();
        }
    }
}