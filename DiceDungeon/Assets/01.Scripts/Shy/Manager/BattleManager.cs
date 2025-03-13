using karin.Charactor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SHY
{
    public class BattleManager : SceneManager
    {
        public static BattleManager Instance;

        public Player player;
        private List<Enemy> enemys = new List<Enemy>();

        internal Action<PlayerData> Initialize;
        public Action playerTurnStart;
        public Action enemyTurnStart;
        

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(this);

            enemyTurnStart += EnemyTurnStart;
        }

        public override void Init(PlayerData _data)
        {
            Debug.Log("Battle Manager Init");
            Initialize.Invoke(_data);
            StartCoroutine(OnPlayerTurn());
        }

        private IEnumerator OnPlayerTurn()
        {
            player.TurnReset();

            for (int i = 0; i < enemys.Count; i++)
            {
                //if(enemys[i])

                //에너미 죽은 놈들 없애고
                yield return new WaitForSeconds(0.5f);
                enemys[i].TurnReset();
                enemys[i].PlayMove();
            }

            yield return new WaitForSeconds(1f);

            playerTurnStart?.Invoke();
        }

        private void EnemyTurnStart() => StartCoroutine(OnEnemyTurn());

        private IEnumerator OnEnemyTurn()
        {
            //enemy들의 행동
            Debug.Log("Enemy 행동");

            foreach (Enemy en in enemys)
            {
                en.PlayAttack();
                yield return new WaitForSeconds(1.3f);
            }

            yield return new WaitForSeconds(1.2f);
            StartCoroutine(OnPlayerTurn());
        }
    }
}