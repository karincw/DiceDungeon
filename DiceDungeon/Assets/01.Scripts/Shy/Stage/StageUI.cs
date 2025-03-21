using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SHY
{
    public class StageUI : MonoBehaviour, IPointerClickHandler
    {
        public StageSO data;
        public List<StageUI> childs = new List<StageUI>();
        private StageManager stageManager;
        private Image img;

        private void Awake()
        {
            img = GetComponent<Image>();
        }

        public void Init(StageSO _data)
        {
            if(stageManager == null) stageManager = FindFirstObjectByType<StageManager>();

            data = _data;
            if(data.icon != null)
                img.sprite = data.icon;

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
            stageManager.MoveCheck(this);
        }
    }
}