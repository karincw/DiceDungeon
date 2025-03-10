using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SHY
{
    public class DiceManager : MonoBehaviour
    {
        public List<UIDice> dices;
        [SerializeField] private BarrelShaker shaker;
        [SerializeField] private BattleManager battleManager;
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

            battleManager.Initialize += Init;
        }

        public void Init(PlayerData _data)
        {
            for (int i = 0; i < 5; i++)
                dices[i].Init(_data.dices[i]);

            shaker.Disappear(0, 0);
        }

        public void TurnInit()
        {
            rollcnt = 3;
            Roll(true);
        }

        public void Roll(bool _resetAll = false)
        {
            if (rollcnt-- <= 0) return;

            for (int i = 0; i < dices.Count; i++)
            {
                if(dices[i].SelectCheck() || _resetAll)
                {
                    dices[i].transform.position = Vector3.zero;
                    dices[i].Roll();
                }
            }
            shaker.Shake();
        }

        public void ReturnDice()
        {
            foreach (UIDice item in dices)
            {
                item.ReturnPos(.4f);
            }
            shaker.Disappear();
        }

        public void UseDice(Player _p)
        {
            foreach (UIDice item in dices)
            {
                item.diceData.OnUse(_p);
            }
        }
    }
}

