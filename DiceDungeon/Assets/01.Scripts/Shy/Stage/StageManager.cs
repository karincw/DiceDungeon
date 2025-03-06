using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public float yDistance = 5;
    public int yStageCnt = 1;
    [SerializeField] private StageSO baseStage, bossStage;
    public List<StageSO> otherStageSO;

    [SerializeField] private StageUI stageUI;
    [SerializeField] private Transform backGround;

    StageUI[,] stageTree = { { } };


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            for (int i = backGround.transform.childCount; i > 0; i--)
            {
                Destroy(backGround.transform.GetChild(i - 1).gameObject);
            }
            MapGenerate();
        }
    }

    public void MapGenerate()
    {
        StageUI start =  Instantiate(stageUI, backGround);
        start.transform.position = Vector3.zero;
        start.Init(baseStage, null);
        stageTree[0, 0] = start;

        float y = 0, aX;

        for (int i = 1; i <= yStageCnt; i++)
        {
            y -= yDistance;

            int xCnt = Random.Range(1, 5);
            aX = (5 - (5 - xCnt) * 0.5f) * 2;
            Vector2 x = new Vector2(-aX / 2, aX / xCnt - aX / 2);

            Debug.Log("Case " + i);

            for (int j = 0; j < xCnt; j++)
            {
                #region »ý¼º
                StageUI stage = Instantiate(stageUI, backGround);

                float calc = aX / xCnt * j;
                stage.transform.position = new Vector3(
                    Random.Range(x.x + calc + 1f, x.y + calc), Random.Range(y + .2f, y - .3f), 0);
                #endregion

                //stage.Init(GetStage(),  q);


                stageTree[i, j] = stage;

            }
        }
    }

    private StageSO GetStage() => otherStageSO[Random.Range(0, otherStageSO.Count)];
}
