using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SHY
{
    public class StageUI : MonoBehaviour, IPointerClickHandler
    {
        public StageSO data;
        public int chlidPos;
        public List<StageUI> parents = new List<StageUI>();

        public void LineRender()
        {
            if (parents[0] == null) return;

            LineRenderer lr = Pooling.Instance.GetItem(PoolEnum.LR, transform).GetComponent<LineRenderer>();
            lr.positionCount = parents.Count * 2;
            for (int i = 0; i < parents.Count; i++)
            {
                lr.SetPosition(0 + i * 2, Vector3.zero);
                lr.SetPosition(1 + i * 2, parents[i].transform.localPosition - transform.localPosition);
            }
        }

        public void Init(StageSO _data, StageUI[] _parents, StageUI[] _childs, bool _isLine = false)
        {
            data = _data;
            //parents = _parents;
            //childs = _childs;

            if (!_isLine) return;

            for (int i = 0; i < _parents.Length; i++)
            {
                LineRenderer lr = Pooling.Instance.GetItem(PoolEnum.LR, transform).GetComponent<LineRenderer>();
                //lr.positionCount = 4;
                //lr.SetPosition(0, Vector3.zero);
                //lr.SetPosition(1, _parents[i].transform.localPosition - transform.localPosition);
                //lr.SetPosition(2, Vector3.zero);
                //lr.SetPosition(3, _childs[0].transform.localPosition - transform.localPosition);

                Debug.Log($"{transform.position} / {_parents[i].transform.position}");
                Debug.Log($"{transform.localPosition} / {_parents[i].transform.localPosition}");
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("Click");
            PlayerImg.Instance.DoMove(transform.position);
        }
    }
}