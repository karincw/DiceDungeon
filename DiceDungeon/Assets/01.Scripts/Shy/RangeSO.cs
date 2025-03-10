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

    public void Get()
    {

    }
}
