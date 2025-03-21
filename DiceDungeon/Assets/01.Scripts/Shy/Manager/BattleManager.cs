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
        private int loopCnt = 0;

        public Action playerTurnInit;
        public Action playerStart;
        public Action<bool> enemyAction;

        public Action<PlayerData> Initialize;
        public Action<Agent> dieEvent;

        [Header("0 : Player, 1 : Enemy")]
        [SerializeField] private List<Color> signColor;
        [SerializeField] private Sprite playerIcon;

        private List<ShowerData> showerDatas = new List<ShowerData>();
        [SerializeField] private Transform showerPos;
        private List<TurnShower> tsList = new List<TurnShower>();
        

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(this);

            enemyAction += ShowerPop;
            enemyAction += (attack) => StartCoroutine(OnEnemyAction(attack));
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
            TurnReset();
        }

        private void ShowerSet()
        {
            showerDatas.Add(new ShowerData(signColor[0], playerIcon));

            for (int i = 0; i < enemys.Count; i++)
            {
                showerDatas.Add(new ShowerData(signColor[1], enemys[i]._eData.icon));
            }
        }

        private void OnShower()
        {
            for (int i = 0; i < showerDatas.Count; i++)
            {
                TurnShower ts = Pooling.Instance.GetItem(PoolEnum.TurnShower, showerPos, false).GetComponent<TurnShower>();
                tsList.Add(ts);
                ts.UpdateImg(showerDatas[i]);
            }
        }

        private void ShowerPop(bool _pop)
        {
            if (_pop) TurnShowerPop();
        }

        private void TurnShowerPop()
        {
            Debug.Log("pop");
            Pooling.Instance.ReturnItem(tsList[0].gameObject);
            tsList.RemoveAt(0);
            showerDatas.RemoveAt(0);
        }


        private void TurnReset()
        {
            player.TurnReset();

            //Turn Show
            ShowerSet();
            OnShower();

            enemyAction.Invoke(false);
        }

        private IEnumerator OnEnemyAction(bool _isAttack)
        {
            if (loopCnt == enemys.Count)
            {
                loopCnt = 0;

                if(_isAttack) //적 공격이 모두 끝났을 때
                {
                    yield return new WaitForSeconds(0.5f);
                    TurnReset();
                }
                else //적 초기화가 모두 끝났을 때
                {
                    playerTurnInit.Invoke();
                }
            }
            else
            {
                //enemy들의 행동
                Debug.Log(loopCnt + "번 Enemy 행동 " + _isAttack);

                yield return new WaitForSeconds(0.75f);

                if(_isAttack)
                {
                    enemys[loopCnt++].PlayAttack();
                }
                else
                {
                    enemys[loopCnt].TurnReset();
                    enemys[loopCnt++].PlayMove();
                }
            }
        }
    }
}