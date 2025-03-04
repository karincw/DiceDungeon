using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace SHY
{
    public class Dice : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IDragHandler, IDropHandler, IEndDragHandler
    {
        private Vector2 clickPos;
        private bool isDrager = false;
        private GameObject changeDice;

        public void OnDrag(PointerEventData eventData)
        {
            isDrager = true;

            transform.localPosition += new Vector3(eventData.position.x - clickPos.x, eventData.position.y - clickPos.y);
            clickPos = eventData.position;

            //if(Vector2.Distance(transform.position, changeDice.transform.position) > 200)
            //{
            //    Debug.Log("돌아와라!");
            //}
        }

        public void OnDrop(PointerEventData eventData)
        {

        }

        public void OnEndDrag(PointerEventData eventData)
        {
            isDrager = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            //주사위 선택
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            clickPos = eventData.position;
        }

        private void OnTriggerEnter2D(Collider2D _col)
        {
            Debug.Log("넣었다" + transform.parent.gameObject.name);

            if (!isDrager) return;

            Debug.Log("깊숙히");

            changeDice = _col.transform.parent.gameObject;
            _col.transform.DOMove(transform.parent.position, 0.3f);
        }
    }
}