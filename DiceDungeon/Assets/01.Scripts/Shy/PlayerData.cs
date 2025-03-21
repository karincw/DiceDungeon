using UnityEngine;

namespace SHY
{
    [CreateAssetMenu(menuName = "SO/Character")]
    public class PlayerData : ScriptableObject
    {
        public string characterName = "";
        public float hp = 10;
        public int gold = 0;
        public int chapterNum = 0;
        public DiceSO[] dices = new DiceSO[5];

        public PlayerData Reflect (PlayerData _data)
        {
            characterName = _data.characterName;
            hp = _data.hp;
            gold = _data.gold;
            for (int i = 0; i < 5; i++)
            {
                dices[i] = CreateInstance<DiceSO>();
                dices[i].Reflect(_data.dices[i]);
            }

            return this;
        }
    }
}