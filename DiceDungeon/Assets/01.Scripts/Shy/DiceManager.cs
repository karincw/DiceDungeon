using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SHY
{
    public class DiceManager : MonoBehaviour
    {
        public List<UIDice> dices;
        [SerializeField] private BarrelShaker shaker;
        private int rollcnt;

        private void Awake()
        {
            shaker.shakeFin += ReturnDice;
            shaker.openCup += () => {
                foreach (UIDice item in dices)
                {
                    item.gameObject.SetActive(true);
                    //item À§Ä¡
                }
            };

            BattleManager.Instance.Initialize += Init;
            BattleManager.Instance.playerTurnStart += TurnInit;
        }

        private void Init(PlayerData _data)
        {
            for (int i = 0; i < 5; i++)
                dices[i].Init(_data.dices[i]);

            shaker.Disappear(0, 0);
        }

        private void TurnInit()
        {
            rollcnt = 3;
            Roll(true);
        }

        private void Roll(bool _resetAll = false)
        {
            if (rollcnt-- <= 0) return;

            for (int i = 0; i < dices.Count; i++)
            {
                if(dices[i].SelectCheck() || _resetAll)
                {
                    dices[i].transform.position = new Vector3(Random.Range(-1.8f, 1.8f), Random.Range(-1.8f, 1.9f), 0);
                    dices[i].Roll();
                }
            }
            shaker.Shake();
        }

        private void ReturnDice()
        {
            foreach (UIDice item in dices)
            {
                item.ReturnPos(.4f);
            }
            shaker.Disappear();
        }


        public void DiceTest() => UseDice(null);

        public void UseDice(Player _p)
        {
            foreach (UIDice item in dices)
            {
                item.diceData.OnUse(_p);
            }
        }
    }
}

