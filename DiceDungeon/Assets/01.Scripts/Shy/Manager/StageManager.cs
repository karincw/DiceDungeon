using System.Collections.Generic;
using UnityEngine;

namespace SHY
{
    public class StageManager : SceneManager
    {
        [SerializeField] private List<ChapterData> chapterDatas;
        internal ChapterData nowChapter;

        private StageUI[,] stageTree = new StageUI[1, 5];
        private List<StageUI> stageList;
        [SerializeField] private Transform backgroundTrm;
        [SerializeField] private MapType iconOrder;
        public List<Sprite> mapIcons;

        private Vector2Int playerPos;

        public void MoveCheck(StageUI _stage)
        {
            bool canMove = false;

            for (int x = 0; x < 5; x++)
            {
                if(stageTree[playerPos.y + 1, x] == _stage)
                {
                    playerPos = new Vector2Int(x, playerPos.y + 1);
                    canMove = true;
                    break;
                }
            }

            if (!canMove) return;

            StagePlayer.Instance.DoMove(stageTree[playerPos.y, playerPos.x].transform.position);
            GameManager.Instance.playerData.nowStage = stageTree[playerPos.y, playerPos.x].data;
        }

        public override void Init(PlayerData _data)
        {
            if (nowChapter == null) NewChapter(_data);
        }

        #region 생성
        private void NewChapter(PlayerData _data)
        {
            nowChapter = chapterDatas[_data.chapterNum].Reflect();

            stageTree = new StageUI[nowChapter.yStageCnt + 1, 5];
            stageList = new List<StageUI>();

            //Base Stage Make
            StageUI fr = Pooling.Instance.GetItem(PoolEnum.StageUI, backgroundTrm).GetComponent<StageUI>();
            fr.transform.position = new Vector3(0, 5);
            StagePlayer.Instance.transform.position = fr.transform.position;
            stageTree[0, 2] = fr;

            //Boss Stage Make
            StageUI la = Pooling.Instance.GetItem(PoolEnum.StageUI, backgroundTrm).GetComponent<StageUI>();
            la.transform.position = new Vector3(0, -nowChapter.yDistance * nowChapter.yStageCnt + 5, 0);
            stageTree[nowChapter.yStageCnt, 2] = la;

            stageList.Add(fr);

            for (int i = 0; i < 5; i++) Gener2();

            fr.Init(nowChapter.baseStage);

            for (int i = 1; i < stageList.Count; i++)
            {
                Debug.Log(i + "번째 Init");
                stageList[i].Init(nowChapter.GetValue());
            }

            stageList.Add(la);
            //la.Init(bossStage);

            playerPos = new Vector2Int(2, 0);
        }

        private void Gener2()
        {
            int curX = 2;
            StageUI st;

            for (int y = 1; y < nowChapter.yStageCnt; y++)
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
                    st = Pooling.Instance.GetItem(PoolEnum.StageUI, backgroundTrm).GetComponent<StageUI>();
                    stageList.Add(st);
                }
                else
                {
                    st = stageTree[y, curX];
                }

                par.childs.Add(st);

                st.transform.position = new Vector3(
                    (curX - 2) * 3 + Random.Range(-.3f, .4f),
                    nowChapter.yDistance * y * -1 + Random.Range(-.3f, .4f) + 5
                    , 0 );

                stageTree[y, curX] = st;
            }

            stageTree[nowChapter.yStageCnt - 1, curX].childs.Add(stageTree[nowChapter.yStageCnt, 2]);
        }
        #endregion
    }
}