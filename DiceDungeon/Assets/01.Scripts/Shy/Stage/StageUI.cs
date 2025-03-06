using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StageUI : MonoBehaviour, IPointerClickHandler
{
    public StageSO data;
    public StageUI[] parents;

    public void Init(StageSO _data, StageUI[] _parents)
    {
        data = _data;
        parents = _parents;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click");
    }
}
