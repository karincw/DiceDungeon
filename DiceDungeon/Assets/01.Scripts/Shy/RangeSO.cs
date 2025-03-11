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

    private Vector2[,] GetV()
    {
        Vector2[,] vArr = new Vector2[7, 7];

        for (int y = 0; y < 7; y++)
        {
            int half = Mathf.CeilToInt(arrX[y] * .5f);
            
            for (int x = 0; x < arrX[y]; x++)
            {
                int xValue = x;
                if (y % 2 == 0) xValue = x - x / half;

                vArr[y, x] = new Vector2(xValue - half + 1, 3 - y);
            }
        }

        //string s = "";
        //for (int y = 0; y < 7; y++)
        //{
        //    for (int x = 0; x < arrX[y]; x++) s += _v[y, x].x;

        //    s += "\n";
        //}
        //Debug.Log(s);

        return vArr;
    }

    public List<Vector2> Get()
    {
        List<Vector2> _arr = new List<Vector2>();
        Vector2[,] vArr = GetV();

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
