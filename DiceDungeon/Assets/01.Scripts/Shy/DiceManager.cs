using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SHY
{
    public class DiceManager : MonoBehaviour
    {
        public List<Dice> dices;
        [SerializeField] private BarrelShaker shaker;

        private void Awake()
        {
            shaker.ShakeFin += RetrunDice;
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                shaker.Shake();
            }
        }



        public void Roll()
        {
            
        }

        public IEnumerator ReturnDice()
        {
            yield return new WaitForSeconds(1.3f);

            foreach (Dice item in dices)
            {
                item.ReturnPos();
            }
        }

        public void RetrunDice() => StartCoroutine(ReturnDice());
    }
}

