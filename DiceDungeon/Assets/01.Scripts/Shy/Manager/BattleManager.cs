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
        internal List<Enemy> enemys = new List<Enemy>();
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
        private List<TurnShower> tsList = new List<TurnShower>();
        
        [Header("UI Pos")]
        [SerializeField] private Transform showerPos;
        [SerializeField] private Transform enemySpawn;
        [SerializeField] private DeadPanel _deadPanel;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(this);

            enemyAction += ShowerPop;
            enemyAction += (attack) => StartCoroutine(OnEnemyAction(attack));
            dieEvent += AgentDie;
        }

        public override void Init(PlayerData _data)
        {
            if (enemySpawn.childCount != 0) Destroy(enemySpawn.GetChild(0).gameObject);

            GameObject spawner = Instantiate((_data.nowStage as BattleStageSO).prefab, enemySpawn);
            Initialize.Invoke(_data);

            if (enemys.Count != 0) enemys.Clear();
            showerDatas = new List<ShowerData>();

            enemys = spawner.GetComponentsInChildren<Enemy>().ToList();

            player.MoveStart(karin.Direction.Right, false);
            player.transform.localPosition = new Vector3(0, 0.8f, 0);
            player.MoveEnd();

            TurnReset();
        }

        private void AgentDie(Agent _agent)
        {
            if(_agent is Player player)
            {
                _deadPanel.openPanel();
                return;
            }
            Enemy en = _agent as Enemy;
            enemys.Remove(en);
            en.DeadEvent();

            for (int i = 0; i < showerDatas.Count; i++)
            {
                if(showerDatas[i].data == en)
                {
                    Destroy(tsList[i].gameObject);
                    tsList.RemoveAt(i);
                    showerDatas.RemoveAt(i);
                }
            }

            Destroy(_agent.gameObject);

            if (enemys.Count == 0)
            {
                Debug.Log("Stage Clear");
                //��� â

                if (GameManager.Instance.playerData.chapterNum == 5) UnityEngine.SceneManagement.SceneManager.LoadScene("Ending");

                Reward.RewardManager.Instance.OnPlay();
            }
        }

        #region Turn Shower
        private void ShowerSet()
        {
            showerDatas.Add(new ShowerData(signColor[0], playerIcon, player));

            for (int i = 0; i < enemys.Count; i++)
            {
                showerDatas.Add(new ShowerData(signColor[1], enemys[i]._eData.icon, enemys[i]));
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
            Pooling.Instance.ReturnItem(tsList[0].gameObject);
            tsList.RemoveAt(0);
            showerDatas.RemoveAt(0);
        }
        #endregion

        #region Turn
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
            yield return new WaitForEndOfFrame();

            if (loopCnt >= enemys.Count)
            {
                loopCnt = 0;

                if(enemys.Count != 0)
                {
                    if (_isAttack) //�� ������ ��� ������ ��
                    {
                        yield return new WaitForSeconds(0.5f);
                        TurnReset();
                    }
                    else //�� �ʱ�ȭ�� ��� ������ ��
                    {
                        playerTurnInit.Invoke();
                    }
                }
            }
            else
            {
                //enemy���� �ൿ
                Debug.Log(loopCnt + "�� Enemy �ൿ " + _isAttack);

                yield return new WaitForSeconds(0.75f);

                if(_isAttack)
                {
                    Debug.Log(enemys[loopCnt]);
                    enemys[loopCnt++].PlayAttack();
                }
                else
                {
                    enemys[loopCnt].TurnReset();
                    enemys[loopCnt++].PlayMove();
                }
            }
        }
        #endregion
    }
}