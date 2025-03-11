using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SHY
{
    public class StageUI : MonoBehaviour, IPointerClickHandler
    {
        public StageSO data;
        public List<StageUI> childs = new List<StageUI>();
        private StageManager stageManager;

        public void Init()
        {
            if(stageManager == null) stageManager = FindFirstObjectByType<StageManager>();

            LineRender();
        }

        public void LineRender()
        {
            if (childs.Count == 0) return;

            LineRenderer lr = Pooling.Instance.GetItem(PoolEnum.LR, transform).GetComponent<LineRenderer>();
            lr.positionCount = childs.Count * 2;
            for (int i = 0; i < childs.Count; i++)
            {
                lr.SetPosition(0 + i * 2, Vector3.zero);
                lr.SetPosition(1 + i * 2, childs[i].transform.localPosition - transform.localPosition);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            //PlayerImg.Instance.DoMove(transform.position);

            stageManager.Move(this);
        }
    }
}