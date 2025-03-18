using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace karin.BuffSystem
{
    public class BuffContainer : MonoBehaviour
    {
        private List<BuffSO> _buffs = new();
        [SerializeField] private List<BuffInterface> _interfaces;

        public BuffSO GetBuff(Buff buffType)
        {
            return _buffs.Find(b => b.buffType == buffType);
        }
        public BuffSO GetBuff(BuffSO buffSO)
        {
            return _buffs.Find(b => b == buffSO);
        }

        public bool Contains(Buff buffType, out BuffSO buff)
        {
            var buffTypeList = _buffs.Select(b => b.buffType).ToList();
            bool result = buffTypeList.Contains(buffType);
            buff = result ? GetBuff(buffType) : null;
            return result;
        }
        public bool Contains(Buff buffType)
        {
            return _buffs.Select(b => b.buffType).Contains(buffType);
        }

        public void AddBuff(BuffSO buffSO, int value)
        {
            //기존에 버프가 있다면
            var buff = GetBuff(buffSO);
            if (buff != null)
            {
                //값을 업데이트하고
                buff.value += value;
                //가지고있는 모든 인터페이스들에게 업데이트 시킴
                _interfaces.ForEach(T => T.UpdateIcon(buff.icon, buff.value));
                return;
            }
            buffSO.value = value;
            _buffs.Add(buffSO);
            _interfaces.ForEach(T => T.AddIcon(buffSO.icon, value));
        }

        public void RemoveBuff(BuffSO buffSO)
        {
            _interfaces.ForEach(T => T.RemoveIcon(buffSO.icon));
            _buffs.Remove(buffSO);
        }
        public void RemoveBuff(Buff buffType)
        {
            BuffSO buffSO = GetBuff(buffType);
            if (buffSO == null) return;
            _interfaces.ForEach(T => T.RemoveIcon(buffSO.icon));
            _buffs.Remove(buffSO);
        }

        public void TurnReset()
        {
            List<BuffSO> removeList = new List<BuffSO>();
            for (int i = 0; i < _buffs.Count; i++)
            {
                _buffs[i].TurnEndSetting();
                _interfaces.ForEach(T => T.UpdateIcon(_buffs[i].icon, _buffs[i].value));
                if (_buffs[i].value <= 0)
                {
                    removeList.Add(_buffs[i]);
                }
            }
            for (int i = 0; i < removeList.Count; i++)
            {
                RemoveBuff(removeList[i]);
            }
        }
    }
}