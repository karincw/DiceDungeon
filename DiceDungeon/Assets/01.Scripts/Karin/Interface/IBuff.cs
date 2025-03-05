using Karin.Charactor;
using UnityEngine;

namespace Karin.Buff
{
    public interface IBuff
    {
        public Agent owner { get; set; }
        public int value { get; set; }
        public void TurnEndSetting();
    }
}