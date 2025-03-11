using Karin.Charactor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SHY
{
    public class BattleManager : SingleTon<BattleManager>
    {
        public Player player;
        public List<Enemy> enemys;

        public Action<PlayerData> Initialize;
        public Action playerTurnStart;
        public Action enemyTurnStart;

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                playerTurnStart?.Invoke();
            }
        }

        private void Awake()
        {
            playerTurnStart += OnPlayerTurn;
            enemyTurnStart += OnEnemyTurn;
        }

        private void OnPlayerTurn()
        {
            Debug.Log("Reset");

            player.TurnReset();

            for (int i = 0; i < enemys.Count; i++)
            {
                //if(enemys[i])

                //에너미 죽은 놈들 없애고
                enemys[i].TurnReset();
                enemys[i].PlayMove();
            }
        }

        

        private IEnumerator Tester()
        {
            yield return new WaitForSeconds(0);
        }

        private void OnEnemyTurn()
        {
            //enemy들의 행동
            Debug.Log("Enemy 행동");

            for (int i = 0; i < enemys.Count; i++)
            {
                //if(enemys[i])

                //에너미 죽은 놈들 없애고
                enemys[i].PlayAttack();
            }

            playerTurnStart.Invoke();
        }
    }
}