using Karin.Charactor;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SHY
{
    public class BattleManager : MonoBehaviour
    {
        public Player player;
        public List<Agent> enemys;

        public Action<PlayerData> Initialize;

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                OnPlayerTurn();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                OnEnemyTurn();
            }
        }


        public void OnPlayerTurn()
        {
            //diceManager.Init();
        }

        public void ExitPlayerTurn()
        {
            //diceManager.UseDice(player);
        }

        public void OnEnemyTurn()
        {

        }
    }
}