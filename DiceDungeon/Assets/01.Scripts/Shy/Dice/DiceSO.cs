using UnityEngine;
using karin.Charactor;
using System.Collections.Generic;
using karin;

namespace SHY
{
    [CreateAssetMenu(menuName = "SO/Shy/Dice/Base")]
    public class DiceSO : ScriptableObject
    {
        [TextArea(1, 2)] public string diceName = "";
        [Space(5)]
        [TextArea(1, 4)] public string description = "";
        [Space(5)]

        public Color diceColor = Color.white;
        public int value;
        public DiceEffect effect;

        public void Reflect(DiceSO _dice)
        {
            eyes = _dice.eyes;
            eyeNum = 0;
        }

        public List<EyeSO> eyes = new();
        private int eyeNum = 0;

        public Sprite Roll()
        {
            eyeNum = Random.Range(0, 6);
            return eyes[eyeNum].icon;
        }

        public void OnUse(Agent _agent) => eyes[eyeNum].OnUse(_agent);
    }
}
