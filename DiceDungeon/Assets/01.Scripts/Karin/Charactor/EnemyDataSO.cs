using SHY;
using System.Collections.Generic;
using UnityEngine;

namespace karin.Charactor
{
    [CreateAssetMenu(menuName = "SO/karin/Charactor/EnemyDataSO")]
    public class EnemyDataSO : ScriptableObject
    {

        public string name = "Dummy";
        public int maxHealth = 10;

        [Range(1, 5)] public int maxMoveCount = 1;
        public List<EyeSO> useAbleAbilitys;

    }
}
