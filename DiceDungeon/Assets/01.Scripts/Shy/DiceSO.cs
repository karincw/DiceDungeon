using UnityEngine;
using Karin.Charactor;

namespace SHY
{
    [CreateAssetMenu(menuName = "SO/Dice")]
    public class DiceSO : ScriptableObject
    {
        public EyeSO[] eyes = new EyeSO[6];
        private int eyeNum = 0;

        public Sprite Roll()
        {
            eyeNum = Random.Range(0, 6);
            return eyes[eyeNum].img;
        }

        public void OnUse(Agent _agent) => eyes[eyeNum].OnUse(_agent);
    }
}
