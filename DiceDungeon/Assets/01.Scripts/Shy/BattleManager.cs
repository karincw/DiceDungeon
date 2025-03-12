using Karin.Charactor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SHY
{
    public class BattleManager : SingleTon<BattleManager>
    {
        public Player player;
        public List<Enemy> enemys;
        private List<Enemy> attackEnemys = new List<Enemy>();

        public Action<PlayerData> Initialize;
        public Action playerTurnStart;
        public Action enemyTurnStart;

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                StartCoroutine(OnPlayerTurn());
            }
        }

        private void Awake()
        {
            enemyTurnStart += EnemyTurnStart;
        }


        private IEnumerator OnPlayerTurn()
        {
            Debug.Log("Reset");

            player.TurnReset();

            for (int i = 0; i < enemys.Count; i++)
            {
                //if(enemys[i])

                //���ʹ� ���� ��� ���ְ�
                yield return new WaitForSeconds(0.5f);
                enemys[i].TurnReset();
                enemys[i].PlayMove();
            }

            yield return new WaitForSeconds(1f);

            playerTurnStart.Invoke();
        }

        private void EnemyTurnStart() => StartCoroutine(OnEnemyTurn());

        private IEnumerator OnEnemyTurn()
        {
            attackEnemys = enemys.ToList();

            //enemy���� �ൿ
            Debug.Log("Enemy �ൿ");

            foreach (Enemy en in attackEnemys)
            {
                en.PlayAttack();
                yield return new WaitForSeconds(1.3f);
            }


            yield return new WaitForSeconds(1.2f);
            StartCoroutine(OnPlayerTurn());
        }
    }
}