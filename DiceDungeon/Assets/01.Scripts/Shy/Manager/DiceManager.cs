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
            Debug.Log("Dice Manager Awake");

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
            Debug.Log("Dice Manager Init");

            for (int i = 0; i < 5; i++)
                dices[i].Init(_data.dices[i]);
        }

        private void OnTurn()
        {
            Debug.Log("Turn Init");
            rollcnt = 3;
            Roll(true);
        }

        private void RollButton() { if (CanClick.clickAble) Roll(); }

        private void Roll(bool _resetAll = false)
        {
            if (rollcnt <= 0) return;

            rollcnt--;
            CanClick.False();

            for (int i = 0; i < dices.Count; i++)
            {
                if (dices[i].SelectCheck() || _resetAll) dices[i].VInit();
            }

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

            Debug.Log("use dice");

            foreach (DiceUI dice in dices)
            {
                yield return new WaitForSeconds(1.3f);
                dice.diceData.OnUse(BattleManager.Instance.player);
            }

            yield return new WaitForSeconds(0.7f);

            Debug.Log("Use Dice Fin");
            BattleManager.Instance.enemyAction.Invoke(true);
        }
    }
}

