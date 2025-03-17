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
        public Action canPlayerInteract;
        public Action enemyTurnStart;

        [Header("0 : Player, 1 : Enemy")]
        [SerializeField] private List<Color> signColor;
        [SerializeField] private TurnShower[] turnShowers = new TurnShower[5];
        

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(this);

            enemyTurnStart += () => turnShowers[0].gameObject.SetActive(false);
            enemyTurnStart += EnemyTurnStart;
        }

        public override void Init(PlayerData _data)
        {
            Debug.Log("Battle Manager Init");
            Initialize.Invoke(_data);
            enemys = FindObjectsByType<Enemy>(FindObjectsSortMode.None).ToList();
            StartCoroutine(OnPlayerTurn());
        }

        private void TurnImgSet(int _idx)
        {
            turnShowers[_idx].Push(signColor[Mathf.Min(_idx, 1)]);
        }

        private IEnumerator OnPlayerTurn()
        {
            player.TurnReset();

            for (int i = 0; i < 5; i++) turnShowers[i].gameObject.SetActive(false);

            turnShowers[0].gameObject.SetActive(true);


            for (int i = 0; i < enemys.Count; i++)
            {
                //if(enemys[i])

                //에너미 죽은 놈들 없애고
                yield return new WaitForSeconds(0.2f);
                enemys[i].TurnReset();
                enemys[i].PlayMove();

                Debug.Log(i);
                if(i < 4) TurnImgSet(i + 1);
            }

            yield return new WaitForSeconds(0.7f);

            playerTurnStart?.Invoke();
        }

        private void EnemyTurnStart() => StartCoroutine(OnEnemyTurn());

        private IEnumerator OnEnemyTurn()
        {
            //enemy들의 행동
            Debug.Log("Enemy 행동");

            yield return new WaitForSeconds(0.75f);

            for (int i = 0; i < enemys.Count; i++)
            {
                enemys[i].PlayAttack();
                if(i < 4) turnShowers[i + 1].gameObject.SetActive(false);
                yield return new WaitForSeconds(0.8f);
            }

            yield return new WaitForSeconds(0.4f);
            StartCoroutine(OnPlayerTurn());
        }
    }
}