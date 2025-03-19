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
        private int enemyActionNum = 0;

        public Action playerTurnStart;
        public Action canPlayerInteract;
        public Action enemyTurn;
        public Action<PlayerData> Initialize;
        public Action<Agent> dieEvent;

        [Header("0 : Player, 1 : Enemy")]
        [SerializeField] private List<Color> signColor;
        [SerializeField] private Sprite playerIcon;

        private List<ShowerData> showerDatas = new List<ShowerData>();
        [SerializeField] private List<TurnShower> turnShowers = new List<TurnShower>();
        

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(this);

            enemyTurn += () => StartCoroutine(OnEnemyTurn());
            dieEvent += AgentDie;
        }

        private void AgentDie(Agent _agent)
        {
            enemys.Remove(_agent as Enemy);
        }

        public override void Init(PlayerData _data)
        {
            Debug.Log("Battle Manager Init");
            Initialize.Invoke(_data);
            enemys = FindObjectsByType<Enemy>(FindObjectsSortMode.None).ToList();
            StartCoroutine(TurnReset());
        }

        private void TurnShowerSet(int _idx = 0, bool _isPlayer = true)
        {
            //Img Change
            //turnShowers[_idx].Push(signColor[!isEnemy ? 0 : 1]);
            if(_isPlayer)
            {
                
                //showerDatas.Add(new ShowerData {  });
            }
            else
            {
                //enemys[i]._eData.icon;
            }


        }

        private void TurnShowerPop()
        {
            turnShowers[0].gameObject.SetActive(false);
            turnShowers[0].transform.SetAsLastSibling();

            turnShowers.Add(turnShowers[0]);
            turnShowers.RemoveAt(0);
        }

        private IEnumerator TurnReset()
        {
            player.TurnReset();

            //Shower Reset
            //for (int i = 1; i < turnShowers.Count; i++) TurnShowerPop();
            //TurnShowerSet();

            //Enemy Reset + Enemy Shower Add
            for (int i = 0; i < enemys.Count; i++)
            {
                enemys[i].TurnReset();
                enemys[i].PlayMove();

                //TurnShowerSet(i, false);
            }

            //Delay
            yield return new WaitForSeconds(0.7f);

            playerTurnStart?.Invoke();
        }

        private IEnumerator OnEnemyTurn()
        {
            if (enemyActionNum == enemys.Count)
            {
                enemyActionNum = 0;
                StartCoroutine(TurnReset());
            }
            else
            {
                enemyActionNum++;

                //enemy들의 행동
                Debug.Log("Enemy 행동");

                yield return new WaitForSeconds(0.75f);

                //TurnShowerPop();
                enemys[0].PlayAttack();
            }
        }
    }
}