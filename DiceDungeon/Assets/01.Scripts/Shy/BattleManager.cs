using Karin.Charactor;
using System.Collections.Generic;
using UnityEngine;

namespace SHY
{
    public class BattleManager : MonoBehaviour
    {
        [SerializeField] private DiceManager diceManager;
        public Player player;
        public List<Agent> enemys;

        public void Update()
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                Init();
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                OnPlayerTurn();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                OnEnemyTurn();
            }
        }

        public void Init()
        {

        }

        public void OnPlayerTurn()
        {
            diceManager.Init();
        }

        public void ExitPlayerTurn()
        {
            diceManager.UseDice(player);
        }

        public void OnEnemyTurn()
        {

        }
    }
}