using System;
using System.Collections.Generic;
using UnityEngine;

namespace Karin.Buff
{
    public class BuffContainer : MonoBehaviour
    {
        private List<IBuff> _buffs;

        public void AddBuff(IBuff buffType, int value)
        {
            buffType.value = value;
            _buffs.Add(buffType);
        }
        public void RemoveBuff(IBuff buffType)
        {
            _buffs.Remove(buffType);
        }

        public void TurnReset()
        {
            foreach (var buff in _buffs)
            {
                buff.TurnEndSetting();
            }
        }
    }
}