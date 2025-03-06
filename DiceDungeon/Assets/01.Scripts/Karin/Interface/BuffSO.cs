using Karin.Charactor;
using UnityEngine;

namespace Karin.BuffSystem
{
    public abstract class BuffSO : ScriptableObject
    {
        public Agent owner;
        public int value;
        public Sprite icon;
        public abstract void TurnEndSetting();
    }
}