using SHY;
using System.Collections.Generic;
using UnityEngine;

namespace Karin.Charactor
{
    [CreateAssetMenu(menuName = "SO/Karin/Charactor/EnemyDataSO")]
    public class EnemyDataSO : ScriptableObject
    {

        public string name = "Dummy";
        public int maxHealth = 10;

        public int maxMoveCount = 1;
        public List<EyeSO> useAbleAbilitys;
        
    }
}
 