using SHY;
using System.Collections.Generic;
using UnityEngine;

namespace Karin.Charactor
{
    [CreateAssetMenu(menuName = "SO/Karin/Charactor/EnemyDataSO")]
    public class EnemyDataSO : ScriptableObject
    {

        [SerializeField] private string _name = "Dummy";
        [SerializeField] private int _maxHealth = 10;

        [SerializeField] private int _maxMoveCount = 1;
        [SerializeField] private List<EyeSO> useAbleAbilitys;
        
    }
}
 