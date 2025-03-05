using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace SHY
{
    public class UIDice : MonoBehaviour, IPointerClickHandler, 
        IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private Vector2 clickPos;
        private bool isDrager = false;
        private int sibleIdx;

        private Sequence seq;

        private void Awake()
        {
            seq = DOTween.Sequence();
        }

        private bool ACheck(Transform _trm) => _trm.GetChild(0).gameObject.activeSelf;

        public bool isRoll() => ACheck(transform);

        public void OnPointerClick(PointerEventData eventData)
        {
            if (isDrager) return;

            transform.GetChild(0).gameObject.SetActive(!ACheck(transform));
        }

        public void ReturnPos(float t = 0.1f)
        {
            transform.DOLocalMove(Vector2.zero, t);
        }


        #region Dice Move
        public void OnBeginDrag(PointerEventData eventData)
        {
            clickPos = eventData.position;

            isDrager = true;

            if (ACheck(transform)) return;
            sibleIdx = transform.parent.GetSiblingIndex();
            transform.parent.SetAsLastSibling();
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!isDrager || ACheck(transform)) return;

            transform.localPosition += new Vector3(eventData.position.x - clickPos.x, eventData.position.y - clickPos.y);
            clickPos = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            ReturnPos();

            if (!ACheck(transform))
            {
                transform.parent.SetSiblingIndex(sibleIdx);
            }
            
            isDrager = false;
        }

        private void OnTriggerEnter2D(Collider2D _col)
        {
            if (!isDrager || ACheck(_col.transform)) return;

            Transform newParent = _col.transform.parent;
            _col.transform.SetParent(transform.parent);
            transform.SetParent(newParent);

            seq.Append(_col.transform.DOLocalMove(Vector3.zero, 0.1f));

            _col.transform.parent.SetSiblingIndex(sibleIdx);
            sibleIdx = transform.parent.GetSiblingIndex();
            transform.parent.SetAsLastSibling();
        }
        #endregion
    }
}