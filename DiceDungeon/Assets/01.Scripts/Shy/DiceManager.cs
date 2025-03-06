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
                }
            };
        }

        public void Init()
        {
            rollcnt = 3;
            shaker.Disappear(0, 0);
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
                    dices[i].gameObject.SetActive(false);
                    dices[i].Init();
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

