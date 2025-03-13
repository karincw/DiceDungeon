using UnityEngine;
using karin.Charactor;
using System.Collections.Generic;

namespace SHY
{
    [CreateAssetMenu(menuName = "SO/Dice")]
    public class DiceSO : ScriptableObject
    {
        // �ֻ��� ���� �̸�
        // �ֻ��� 
        // �ֻ��� ����
        // �ֻ��� �������� ��

        public void  Reflect(DiceSO _dice) 
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
