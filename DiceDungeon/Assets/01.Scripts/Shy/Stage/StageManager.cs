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
                st.transform.position = new Vector3((curX - 2) * 3, yDistance * y * -1, 0 );

                stageTree[y, curX] = st;
                st.LineRender();
            }
        }

        public void MapGenerate()
        {
            stageTree = new StageUI[yStageCnt + 2, 5];
            float xDistance = 3f;

            //StageUI start = Pooling.Instance.GetItem(PoolEnum.StageUI, backGround).GetComponent<StageUI>();
            //start.transform.position = Vector3.zero;
            ////start.Init(baseStage, null, null);
            //
            //stageTree[0, 2] = start;
            int ra = RandByArr(new int[] { 15, 100, 70 }) + 2, curX = 0;


            for (int i = 0; i < 6; i++)
            {
                Gener2();
            }
            



            //float yPos = 0, aX;

            //for (int yNum = 1; yNum <= yStageCnt; yNum++)
            //{
            //    yPos -= yDistance;

            //    int xCnt = RandByArr(new int[] { 15, 100, 70, 10 });

            //    aX = (5 - (5 - xCnt) * 0.5f) * 2;
            //    Vector2 xPosAbs = new Vector2(-aX / 2, aX / xCnt - aX / 2);

            //    #region 생성
            //    for (int xNum = 0; xNum < xCnt; xNum++)
            //    {
            //        StageUI stage = Pooling.Instance.GetItem(PoolEnum.StageUI, backGround).GetComponent<StageUI>();

            //        float calc = aX / xCnt * xNum;
            //        stage.transform.position = new Vector3(
            //            Random.Range(xPosAbs.x + calc + xDistance, xPosAbs.y + calc) - xDistance / 2, 
            //            Random.Range(yPos + .2f, yPos - .3f), 0);
            //        //stageTree[yNum].Add(stage);
            //    }
            //    #endregion
            //}

            ////마지막 아래 생성
            //yPos -= yDistance;
            //StageUI lastStage = Pooling.Instance.GetItem(PoolEnum.StageUI, backGround).GetComponent<StageUI>();
            //lastStage.transform.position = new Vector3(0, yPos, 0);
            //stageTree[yStageCnt + 1, 2] = lastStage;





            #region 연결

            //StageConnect(0);

            //for (int y = 1; y < stageTree.Length - 1; y++)
            //{
            //    for (int x = 0; x < stageTree[y].Count; x++)
            //    {
            //        //윗 놈들을 모아서
            //        List<StageUI> parentTrees = stageTree[y - 1].ToList();
            //        List<StageUI> childTrees = stageTree[y + 1].ToList();

                    
            //        //윗 놈들 개수를 구하고
            //        int childCnt = childTrees.Count; // 1~4

            //        if (childCnt >= 2) childCnt = RandByArr(new int[] { 190, 10 });

            //        while (1 < parentTrees.Count)
            //        {
            //            parentTrees.RemoveAt(Random.Range(0, parentTrees.Count));
            //        }

            //        while (childCnt < childTrees.Count)
            //        {
            //            childTrees.RemoveAt(Random.Range(0, childTrees.Count));
            //        }

            //        stageTree[y][x].Init(GetStage(), parentTrees.ToArray(), childTrees.ToArray(), true);
            //    }


            //}
            #endregion
        }

        //public void StageConnect(int _yValue)
        //{
        //    if (_yValue == stageTree.Length) return;

        //    foreach (var stage in stageTree[_yValue])
        //    {

        //    }

        //    //StageConnect(_yValue + 1);
        //}


    }

}