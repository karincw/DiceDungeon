using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SHY
{
    [CreateAssetMenu(menuName = "SO/Shy/Stage/Chapter")]
    public class ChapterData : ScriptableObject
    {
        public float yDistance = 3;
        public int yStageCnt = 15;
        [SerializeField] private int easyStage = 3;
        [SerializeField] private int eliteStage = 7;

        [Header("StageSO")]
        public StageSO baseStage, bossStage;
        [SerializeField] private List<StageSO> easyEnemyStage;
        [SerializeField] private List<StageSO> normalEnemyStage;
        [SerializeField] private List<StageSO> eliteEnemyStage;
        [SerializeField] private List<StageSO> eventsStage;
        [SerializeField] private List<StageSO> marketStage;

        [Header("Reward List")]
        [SerializeField] private List<EyeSO> rewards;

        
        [Header("Spawn Ratio")]
        [SerializeField] private int normalEnemy = 8;
        [SerializeField] private int eliteEnemy = 2;
        [SerializeField] private int market = 1;
        [SerializeField] private int events = 4;

        public ChapterData Reflect()
        {
            ChapterData ch = CreateInstance<ChapterData>();
            ch.yDistance = yDistance;
            ch.yStageCnt = yStageCnt;
            ch.easyStage = easyStage;
            ch.eliteStage = eliteStage;
            ch.baseStage = baseStage;
            ch.bossStage = bossStage;
            ch.normalEnemyStage = new List<StageSO>(normalEnemyStage);
            ch.eliteEnemyStage = new List<StageSO>(eliteEnemyStage);
            ch.eventsStage = new List<StageSO>(eventsStage);
            ch.marketStage = new List<StageSO>(marketStage);
            ch.rewards = new List<EyeSO>(rewards);
            ch.normalEnemy = normalEnemy;
            ch.eliteEnemy = eliteEnemy;
            ch.market = market;
            ch.events = events;

            return ch;
        }

        #region Stage Maker
        public StageSO GetValue()
        {
            int rand = 0, res = 0;

            if (normalEnemyStage.Count != 0)
            {
                rand += normalEnemy * 1000;
                res += normalEnemy;
            }
            if (eliteEnemyStage.Count != 0)
            {
                rand += eliteEnemy * 100;
                res += eliteEnemy;
            }
            if (marketStage.Count != 0)
            {
                rand += market * 10;
                res += market;
            }
            if (eventsStage.Count != 0)
            {
                rand += events;
                res += events;
            }

            if(rand == 0)
            {
                Debug.Log("Error : List가 모두 비었습니다.");
                return null;
            }

            res = Random.Range(0, res);
            Debug.Log(res);

            for (int i = 0; i < 4; i++)
            {
                res -= rand % 10;
                if(res < 0)
                {
                    rand = i;
                    break;
                }
                rand /= 10;
            }

            if (rand == 0) return SelectItem(eventsStage); //1
            if (rand == 1) return SelectItem(marketStage); //0
            if (rand == 2) return SelectItem(eliteEnemyStage); //1
            if (rand == 3) return SelectItem(normalEnemyStage); //0

            Debug.Log("Error by ChapterData");
            return null;
        }

        private StageSO SelectItem(List<StageSO> _stages)
        {
            StageSO target = _stages[Random.Range(0, _stages.Count)];
            _stages.Remove(target);
            return target;
        }
        #endregion

        public List<EyeSO> GetRewards()
        {
            List<EyeSO> reward = rewards.ToList();

            int min = Mathf.Min(reward.Count, 5);

            if (min <= 4) return reward;

            List<EyeSO> results = new List<EyeSO>();

            for (int i = 0; i < 4; i++)
            {
                int rand = Random.Range(0, reward.Count);
                results.Add(reward[rand]);
                reward.RemoveAt(rand);
            }

            return results;
        }
    }
}
