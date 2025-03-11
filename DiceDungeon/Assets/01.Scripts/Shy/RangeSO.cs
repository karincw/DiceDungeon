using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Range")]
public class RangeSO : ScriptableObject
{
    [HideInInspector] public List<bool> arr = new List<bool>(37);
    [HideInInspector] public int[] arrX = { 4, 5, 6, 7, 6, 5, 4 };


    private void OnEnable()
    {
        if(arr.Count < 37)
        {
            arr = new List<bool>();
            for (int i = 0; i < 37; i++)
            {
                arr.Add(false);
            }
        }
    }

    private Vector2Int[,] GetV()
    {
        Vector2Int[,] vArr = new Vector2Int[7, 7];

        for (int y = 0; y < 7; y++)
        {
            int half = Mathf.FloorToInt(arrX[y] * .5f);
            
            for (int x = 0; x < arrX[y]; x++)
            {
                int xValue = x;
                //if (y % 2 == 0) xValue = x - x / half;

                vArr[y, x] = new Vector2Int(xValue - half, 3 - y);
            }
        }

        return vArr;
    }

    public List<Vector2Int> Get()
    {
        List<Vector2Int> _arr = new List<Vector2Int>();
        Vector2Int[,] vArr = GetV();

        int leng = 0;

        for (int y = 0; y < 7; y++)
        {
            for (int x = 0; x < arrX[y]; x++)
            {
                if(arr[leng++])
                {
                    _arr.Add(vArr[y, x]);
                }
            }
        }

        return _arr;
    }
}
