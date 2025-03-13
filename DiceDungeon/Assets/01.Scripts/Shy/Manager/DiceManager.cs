using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SHY
{
    public class DiceManager : MonoBehaviour
    {
        public List<DiceUI> dices;
        [SerializeField] private BarrelShaker shaker;
        [SerializeField] private Vector2[] dicesSpawnPos;
        private int rollcnt;

        private void Awake()
        {
            Debug.Log("Dice Manager Awake");

            shaker.shakeFin += ReturnDice;
            shaker.openCup += () => {
                foreach (DiceUI item in dices)
                {
                    item.gameObject.SetActive(true);
                }
            };

            BattleManager.Instance.Initialize += Init;
            BattleManager.Instance.playerTurnStart += TurnInit;
        }

        private void Init(PlayerData _data)
        {
            Debug.Log("Dice Init");

            for (int i = 0; i < 5; i++)
                dices[i].Init(_data.dices[i]);

            shaker.Disappear(0, 0);
        }

        private void TurnInit()
        {
            Debug.Log("Turn Init");
            rollcnt = 3;
            Roll(true);
        }

        private void RollButton()
        {
            if (CanClick.clickAble) Roll();
        }

        private void Roll(bool _resetAll = false)
        {
            if (rollcnt <= 0 && !_resetAll) return;

            rollcnt--;
            CanClick.False();

            for (int i = 0; i < dices.Count; i++)
            {
                if(dices[i].SelectCheck() || _resetAll)
                {
                    dices[i].transform.position = dicesSpawnPos[i];
                    dices[i].Roll();
                }
            }
            shaker.Shake();
        }

        private void ReturnDice()
        {
            foreach (DiceUI item in dices)
            {
                item.ReturnPos(.25f);
            }
            shaker.Disappear();
            CanClick.True();
        }


        public void UseDices()
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
                yield return new WaitForSeconds(2);
                dice.diceData.OnUse(BattleManager.Instance.player);
            }

            yield return new WaitForSeconds(2.5f);

            BattleManager.Instance.enemyTurnStart.Invoke();
        }
    }
}

