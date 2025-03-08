using System.Collections.Generic;
using System.Linq;
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

        private List<StageUI>[] stageTree;


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


        public void MapGenerate()
        {
            stageTree = new List<StageUI>[yStageCnt + 2];
            float xDistance = 3f;

            StageUI start = Pooling.Instance.GetItem(PoolEnum.StageUI, backGround).GetComponent<StageUI>();
            start.transform.position = Vector3.zero;
            start.Init(baseStage, null, null);

            stageTree[0] = new List<StageUI>();
            stageTree[0].Add(start);

            float yPos = 0, aX;

            for (int yNum = 1; yNum <= yStageCnt; yNum++)
            {
                stageTree[yNum] = new List<StageUI>();

                yPos -= yDistance;

                int xCnt = RandByArr(new int[] { 15, 100, 70, 10 });

                aX = (5 - (5 - xCnt) * 0.5f) * 2;
                Vector2 xPosAbs = new Vector2(-aX / 2, aX / xCnt - aX / 2);

                #region 생성
                for (int xNum = 0; xNum < xCnt; xNum++)
                {
                    StageUI stage = Pooling.Instance.GetItem(PoolEnum.StageUI, backGround).GetComponent<StageUI>();

                    float calc = aX / xCnt * xNum;
                    stage.transform.position = new Vector3(
                        Random.Range(xPosAbs.x + calc + xDistance, xPosAbs.y + calc) - xDistance / 2, 
                        Random.Range(yPos + .2f, yPos - .3f), 0);
                    stageTree[yNum].Add(stage);
                }
                #endregion
            }

            //마지막 아래 생성
            yPos -= yDistance;
            StageUI lastStage = Pooling.Instance.GetItem(PoolEnum.StageUI, backGround).GetComponent<StageUI>();
            lastStage.transform.position = new Vector3(0, yPos, 0);
            stageTree[yStageCnt + 1] = new List<StageUI>() { lastStage };





            #region 연결

            //StageConnect(0);

            for (int y = 1; y < stageTree.Length - 1; y++)
            {
                for (int x = 0; x < stageTree[y].Count; x++)
                {
                    //윗 놈들을 모아서
                    List<StageUI> parentTrees = stageTree[y - 1].ToList();
                    List<StageUI> childTrees = stageTree[y + 1].ToList();

                    
                    //윗 놈들 개수를 구하고
                    int childCnt = childTrees.Count; // 1~4

                    if (childCnt >= 2) childCnt = RandByArr(new int[] { 190, 10 });

                    while (1 < parentTrees.Count)
                    {
                        parentTrees.RemoveAt(Random.Range(0, parentTrees.Count));
                    }

                    while (childCnt < childTrees.Count)
                    {
                        childTrees.RemoveAt(Random.Range(0, childTrees.Count));
                    }

                    stageTree[y][x].Init(GetStage(), parentTrees.ToArray(), childTrees.ToArray(), true);
                }


            }
            #endregion
        }

        //public void StageConnect(int _yValue)
        //{
        //    if (_yValue == stageTree.Length) return;
        //
        //    StageConnect(_yValue + 1);
        //}


    }

}