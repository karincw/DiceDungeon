using Karin.Charactor;
using UnityEngine;

namespace Karin.Buff
{
    public class DeBuffPosion : IBuff
    {
        public Agent owner { get; set; }
        public int value { get; set; }
        private int decreaseValue = 1;

        public void TurnEndSetting()
        {
            owner.health.DecreaseHealth(value);
            value -= decreaseValue;
        }
    }
}