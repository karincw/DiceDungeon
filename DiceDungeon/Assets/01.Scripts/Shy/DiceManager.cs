using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SHY
{
    public class DiceManager : MonoBehaviour
    {
        public List<UIDice> dices;
        [SerializeField] private BarrelShaker shaker;

        private void Awake()
        {
            shaker.ShakeFin += RetrunDice;
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                Roll();
            }
        }



        public void Roll()
        {
            for (int i = 0; i < dices.Count; i++)
            {
                if(dices[i].isRoll())
                {
                    dices[i].transform.position = Vector3.zero;
                }
            }
            shaker.Shake();
        }

        public IEnumerator ReturnDice()
        {
            yield return new WaitForSeconds(1.3f);

            
        }

        public void RetrunDice()
        {
            foreach (UIDice item in dices)
            {
                item.ReturnPos(.4f);
            }
        }
    }
}

