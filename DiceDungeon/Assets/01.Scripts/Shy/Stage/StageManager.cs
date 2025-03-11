using System.Collections.Generic;
using UnityEngine;

namespace SHY
{

    public class StageManager : MonoBehaviour
    {
        [SerializeField] private float yDistance = 3;
        [SerializeField] private int yStageCnt = 15;
        [SerializeField] private StageSO baseStage, bossStage;
        public List<StageSO> otherStageSO;

        [SerializeField] private Transform backGround;

        private StageUI[,] stageTree = new StageUI[1, 5];

        private Vector2 nowPlayerPos;


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                for (int i = backGround.transform.childCount; i > 0; i--)
                {
                    Destroy(backGround.transform.GetChild(i - 1).gameObject);
                }
                MapInit();
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

        #region 생성
        void Gener2()
        {
            int curX = 2;
            StageUI st;

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

                par.childs.Add(st);

                st.transform.position = new Vector3(
                    (curX - 2) * 3 + Random.Range(-.3f, .4f), 
                    yDistance * y * -1 + Random.Range(-.3f, .4f)
                    , 0 );

                stageTree[y, curX] = st;
            }

            stageTree[yStageCnt - 1, curX].childs.Add(stageTree[yStageCnt, 2]);
        }
        #endregion

        public void MapInit()
        {
            stageTree = new StageUI[yStageCnt + 1, 5];

            //맨 윗 놈
            StageUI fr = Pooling.Instance.GetItem(PoolEnum.StageUI, backGround).GetComponent<StageUI>();
            fr.transform.position = Vector3.zero;
            stageTree[0, 2] = fr;

            //Boss Stage Make
            StageUI la = Pooling.Instance.GetItem(PoolEnum.StageUI, backGround).GetComponent<StageUI>();
            la.transform.position = new Vector3(0, -yDistance * yStageCnt, 0);
            stageTree[yStageCnt, 2] = la;

            for (int i = 0; i < 5; i++) Gener2();

            stageTree[0, 2].LineRender();

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

            nowPlayerPos = new Vector2(2, 0);
        }
    }
}