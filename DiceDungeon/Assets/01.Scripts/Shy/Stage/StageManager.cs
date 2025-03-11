using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SHY
{

    public class StageManager : MonoBehaviour
    {
        public float yDistance = 5;
        public int yStageCnt = 1;
        [SerializeField] private StageSO baseStage, bossStage;
        public List<StageSO> otherStageSO;

        [SerializeField] private Transform backGround;

        private StageUI[,] stageTree = new StageUI[1, 5];


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                for (int i = backGround.transform.childCount; i > 0; i--)
                {
                    Destroy(backGround.transform.GetChild(i - 1).gameObject);
                }
                MapGenerate();
            }
        }

        private StageSO GetStage() => otherStageSO[Random.Range(0, otherStageSO.Count)];

        //(확률) = RandByArr({10, 15, 10}) 10% = 1, 15% = 2, 10% = 3
        private int RandByArr(int[] _arr)
        {
            int _va = 0;

            for (int i = 0; i < _arr.Length; i++) _va += _arr[i];

            _va = Random.Range(0, _va);

            for (int i = 0; i < _arr.Length; i++)
            {
                _va -= _arr[i];
                if (_va < 0) return i + 1;
            }

            Debug.LogError("Rand Error");

            return 99;
        }

        void Gener2()
        {
            int curX = 2, rand;

            StageUI st = Pooling.Instance.GetItem(PoolEnum.StageUI, backGround).GetComponent<StageUI>();
            st.transform.position = Vector3.zero;

            stageTree[0, curX] = st;

            for (int y = 1; y < yStageCnt; y++)
            {
                int xMax = Mathf.Min(curX + 1, 4), xMin = Mathf.Max(curX - 1, 0);
                StageUI par = stageTree[y - 1, curX];


                //밑에가 있다면 좌우 비교
                if (stageTree[y, curX] != null)
                {
                    if (stageTree[y - 1, xMin] != null) xMin = curX;
                    if (stageTree[y - 1, xMax] != null) xMax = curX;
                }

                
                //위치 체크
                curX = Random.Range(xMin, xMax + 1);

                if (stageTree[y, curX] == null)
                {
                    st = Pooling.Instance.GetItem(PoolEnum.StageUI, backGround).GetComponent<StageUI>();
                }
                else
                {
                    st = stageTree[y, curX];
                }

                st.parents.Add(par);

                st.transform.position = new Vector3(
                    (curX - 2) * 3 + Random.Range(-.3f, .4f), 
                    yDistance * y * -1 + Random.Range(-.3f, .4f)
                    , 0 );

                stageTree[y, curX] = st;
            }
        }

        public void MapGenerate()
        {
            stageTree = new StageUI[yStageCnt + 2, 5];

            for (int i = 0; i < 5; i++)
            {
                Gener2();
            }

            //Boss Stage Make

            for (int y = 1; y < yStageCnt; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    if(stageTree[y, x] != null)
                    {
                        //여기서 데이터 넣어주면 될 듯
                        stageTree[y, x].LineRender();
                    }
                }
            }
        }
    }
}