using UnityEngine;

namespace SHY
{
    [CreateAssetMenu(menuName = "SO/Character")]
    public class PlayerData : ScriptableObject
    {
        public string characterName = "";
        public float hp = 10;
        public int gold = 0;
        public DiceSO[] dices = new DiceSO[5];

        
        public PlayerData() { }
        public PlayerData(PlayerData _data)
        {
            characterName = _data.characterName;
            hp = _data.hp;
            gold = _data.gold;
            for (int i = 0; i < 5; i++)
            {
                dices[i] = new DiceSO(_data.dices[i]);
            }
        }
    }
}