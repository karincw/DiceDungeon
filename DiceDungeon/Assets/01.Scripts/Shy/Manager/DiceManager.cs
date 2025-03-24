using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace SHY
{
    public class DiceManager : MonoBehaviour
    {
        [SerializeField] private List<DiceUI> dices;
        [SerializeField] private Vector2[] dicesSpawnPos;
        private int rollcnt;
        [SerializeField] private HorizontalLayoutGroup layoutGroup;

        private void Awake()
        {
            layoutGroup.enabled = false;

            BattleManager.Instance.Initialize += Init;
            BattleManager.Instance.playerTurnInit += OnTurn;

            BattleManager.Instance.playerStart += () =>
            {
                layoutGroup.enabled = false;
                CanClick.True();
            };
        }

        private void Init(PlayerData _data)
        {
            for (int i = 0; i < 5; i++)
                dices[i].gameObject.SetActive(false);
        }

        private void OnTurn()
        {
            rollcnt = 3;

            DiceSO[] _data = GameManager.Instance.playerData.dices;

            for (int i = 0; i < 5; i++)
                dices[i].Init(_data[i]);

            Roll(true);
        }

        private void RollButton() { if (CanClick.clickAble) Roll(); }

        private void Roll(bool _resetAll = false)
        {
            if (rollcnt <= 0) return;

            bool isUse = false;

            for (int i = 0; i < dices.Count; i++)
            {
                if (dices[i].SelectCheck() || _resetAll)
                {
                    dices[i].VInit();
                    isUse = true;
                }
            }

            if (!isUse) return;

            CanClick.False();

            rollcnt--;
            StartCoroutine(SpawnDices(_resetAll));
        }

        private IEnumerator SpawnDices(bool _reset)
        {
            for (int i = 0; i < dices.Count; i++)
            {
                if(_reset) layoutGroup.enabled = true;

                if (dices[i].SelectCheck() || _reset)
                {
                    yield return new WaitForSeconds(0.3f);
                    dices[i].Roll();
                }
            }
        }

        private void UseDices()
        {
            if (!CanClick.clickAble) return;

            CanClick.False();
            StartCoroutine(UseDice());
        }

        private IEnumerator UseDice()
        {
            dices = dices.OrderBy(dice => dice.sibleIdx).ToList();

            foreach (DiceUI dice in dices)
            {
                yield return new WaitForSeconds(1.3f);
                dice.diceData.OnUse(BattleManager.Instance.player);
            }

            yield return new WaitForSeconds(0.7f);

            BattleManager.Instance.enemyAction.Invoke(true);
        }
    }
}

