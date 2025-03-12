using UnityEngine;

namespace Karin.BuffSystem
{
    [CreateAssetMenu(menuName = "SO/Karin/Buff/DeBuffPoison")]
    public class DeBuffPosionSO : BuffSO
    {
        public override void TurnEndSetting()
        {
            owner.health.DecreaseHealth(value);
            base.TurnEndSetting();
        }
    }
}