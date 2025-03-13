using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

namespace SHY
{
    public class DiceUI : MonoBehaviour, IPointerClickHandler, 
        IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private Vector2 clickPos;
        private bool isDrager = false;
        internal int sibleIdx;

        public DiceSO diceData;

        private Image icon;
        private GameObject checker;
        

        private void Awake()
        {
            icon = transform.Find("Icon").GetComponent<Image>();
            checker = transform.Find("Check").gameObject;
        }

        private bool SelectCheck(Transform _trm) => _trm.Find("Check").gameObject.activeSelf;

        public bool SelectCheck() => checker.activeSelf;

        public void Init(DiceSO _so)
        {
            diceData = _so;
            Roll();
            sibleIdx = transform.parent.GetSiblingIndex();
        }

        public void Roll()
        {
            icon.sprite = diceData.Roll();
            checker.SetActive(false);
            gameObject.SetActive(false);
        }

        public void ReturnPos(float t = 0.1f)
        {
            transform.DOLocalMove(Vector2.zero, t);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (isDrager || !CanClick.clickAble) return;
            checker.SetActive(!SelectCheck());
        }

        #region Dice Move
        public void OnBeginDrag(PointerEventData eventData)
        {
            clickPos = eventData.position;

            isDrager = true;

            if (SelectCheck() || !CanClick.clickAble) return;
            sibleIdx = transform.parent.GetSiblingIndex();
            transform.parent.SetAsLastSibling();
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!isDrager || !CanClick.clickAble || SelectCheck()) return;

            transform.localPosition += new Vector3(eventData.position.x - clickPos.x, eventData.position.y - clickPos.y);
            clickPos = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            ReturnPos();

            if (!SelectCheck())
                transform.parent.SetSiblingIndex(sibleIdx);

            isDrager = false;
        }

        public void SetSible(int _sible)
        {
            sibleIdx = _sible;
            transform.parent.SetSiblingIndex(sibleIdx);
        }

        private void OnTriggerEnter2D(Collider2D _col)
        {
            if (!isDrager || SelectCheck(_col.transform)) return;

            Transform newParent = _col.transform.parent;
            _col.transform.SetParent(transform.parent);
            transform.SetParent(newParent);

            _col.transform.DOLocalMove(Vector3.zero, 0.1f);

            _col.GetComponent<DiceUI>().SetSible(sibleIdx);
            sibleIdx = transform.parent.GetSiblingIndex();
            transform.parent.SetAsLastSibling();
        }
        #endregion
    }
}