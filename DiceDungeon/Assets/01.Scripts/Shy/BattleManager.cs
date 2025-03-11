using Karin.Charactor;
using System;
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

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                playerTurnStart?.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                OnEnemyTurn();
            }
        }

        private void Awake()
        {
            playerTurnStart += OnPlayerTurn;
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

        private void OnEnemyTurn()
        {
            //enemy들의 행동
        }
    }
}