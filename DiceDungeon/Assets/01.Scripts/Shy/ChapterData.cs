using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SHY
{
    [CreateAssetMenu(menuName = "SO/Shy/Stage/Chapter")]
    public class ChapterData : ScriptableObject
    {
        public float yDistance = 3;
        public int yStageCnt = 15;

        [Header("StageSO")]
        public StageSO baseStage, bossStage;
        [SerializeField] private List<StageSO> normalEnemySO;
        [SerializeField] private List<StageSO> eliteEnemySO;
        [SerializeField] private List<StageSO> eventsStageSO;
        [SerializeField] private List<StageSO> marketStageSO;

        
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
            ch.baseStage = baseStage;
            ch.bossStage = bossStage;
            ch.normalEnemySO = new List<StageSO>(normalEnemySO);
            ch.eliteEnemySO = new List<StageSO>(eliteEnemySO);
            ch.eventsStageSO = new List<StageSO>(eventsStageSO);
            ch.marketStageSO = new List<StageSO>(marketStageSO);
            ch.normalEnemy = normalEnemy;
            ch.eliteEnemy = eliteEnemy;
            ch.market = market;
            ch.events = events;

            return ch;
        }

        public StageSO GetValue()
        {
            int rand = 0, res = 0;

            if (normalEnemySO.Count != 0)
            {
                rand += normalEnemy * 1000;
                res += normalEnemy;
            }
            if (eliteEnemySO.Count != 0)
            {
                rand += eliteEnemy * 100;
                res += eliteEnemy;
            }
            if (marketStageSO.Count != 0)
            {
                rand += market * 10;
                res += market;
            }
            if (eventsStageSO.Count != 0)
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

            if (rand == 0) return SelectItem(eventsStageSO); //1
            if (rand == 1) return SelectItem(marketStageSO); //0
            if (rand == 2) return SelectItem(eliteEnemySO); //1
            if (rand == 3) return SelectItem(normalEnemySO); //0

            Debug.Log("Error by ChapterData");
            return null;
        }

        private StageSO SelectItem(List<StageSO> _stages)
        {
            StageSO target = _stages[Random.Range(0, _stages.Count)];
            _stages.Remove(target);
            return target;
        }
    }
}
