using UnityEngine;
using Karin.Charactor;

namespace SHY
{
    [CreateAssetMenu(menuName = "SO/Dice")]
    public class DiceSO : ScriptableObject
    {
        public DiceSO() { }
        public DiceSO(DiceSO _dice) 
        {
            eyes = _dice.eyes;
            eyeNum = 0;
        }

        public EyeSO[] eyes = new EyeSO[6];
        private int eyeNum = 0;

        public Sprite Roll()
        {
            eyeNum = Random.Range(0, 6);
            return eyes[eyeNum].icon;
        }

        public void OnUse(Agent _agent) => eyes[eyeNum].OnUse(_agent);
    }
}
