using UnityEngine;
using karin.Charactor;
using System.Collections.Generic;
using karin;
using System.Linq;

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

        public List<EyeSO> eyes = new();
        private int eyeNum = 0;

        public DiceSO Reflect()
        {
            DiceSO ds = CreateInstance<DiceSO>();
            for (int i = 0; i < eyes.Count; i++)
            {
                ds.eyes.Add(eyes[i]);
            }
            ds.eyeNum = 0;

            return ds;
        }

        

        public Sprite Roll()
        {
            eyeNum = Random.Range(0, 6);
            return eyes[eyeNum].icon;
        }

        public void OnUse(Agent _agent) => eyes[eyeNum].OnUse(_agent);
    }
}
