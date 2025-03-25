using UnityEngine;

namespace SHY
{
    [CreateAssetMenu(menuName = "SO/Character")]
    public class PlayerData : ScriptableObject
    {
        public float hp = 10;
        public int gold = 0;
        public DiceSO[] dices = new DiceSO[5];

        //Stage
        public int chapterNum = 0;
        internal StageSO nowStage;

        public PlayerData Reflect ()
        {
            PlayerData pd = CreateInstance<PlayerData>();

            pd.hp = hp;
            pd.gold = gold;

            for (int i = 0; i < 5; i++)
            {
                pd.dices[i] = dices[i].Reflect();
            }

            return pd;
        }

        public void DiceInit()
        {
            
        }
    }
}