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
        private Vector2 lastLPos;
        private bool isDrager = false;
        internal int sibleIdx;
        private int loopCnt = 4;
        private static int rollingCnt = 0;

        public DiceSO diceData;

        private Image icon;
        private GameObject checker;
        private Animator anime;
        private BoxCollider2D boxCollider;
        

        private void Awake()
        {
            icon = transform.Find("Icon").GetComponent<Image>();
            checker = transform.Find("Check").gameObject;
            anime = GetComponent<Animator>();
            boxCollider = GetComponent<BoxCollider2D>();
        }

        private bool SelectCheck(Transform _trm) => _trm.Find("Check").gameObject.activeSelf;

        public bool SelectCheck() => checker.activeSelf;

        public void Init(DiceSO _so)
        {
            diceData = _so;
            checker.SetActive(false);
            sibleIdx = transform.GetSiblingIndex();
            lastLPos = transform.localPosition;

            VInit();
        }

        public void VInit()
        {
            icon.sprite = diceData.Roll();
            icon.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }

        public void Roll()
        {
            checker.SetActive(false);
            gameObject.SetActive(true);
            rollingCnt++;
            anime.SetBool("Roll", true);
        }

        public void RollCheck()
        {
            if (--loopCnt == 0)
            {
                anime.SetBool("Roll", false);
                icon.gameObject.SetActive(true);
                loopCnt = 5;

                if(--rollingCnt == 0)
                BattleManager.Instance.playerStart.Invoke();
            }
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

            if (SelectCheck() || !CanClick.clickAble) return;

            isDrager = true;
            transform.SetAsLastSibling();
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!CanClick.clickAble || SelectCheck()) return;

            transform.localPosition += new Vector3(eventData.position.x - clickPos.x, eventData.position.y - clickPos.y);
            clickPos = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            isDrager = false;
            ReturnPos();
        }

        private void PosUpdate(DiceUI _dice)
        {
            //Last LPos 교환
            Vector2 temp = lastLPos;
            lastLPos = _dice.lastLPos;
            _dice.lastLPos = temp;

            //Sibling 교환
            int temp2 = _dice.sibleIdx;
            _dice.sibleIdx = sibleIdx;
            sibleIdx = temp2;

            //위치 전환
            ReturnPos();
        }

        private void ReturnPos(float t = 0.1f)
        {
            boxCollider.enabled = false;
            transform.SetSiblingIndex(sibleIdx);
            transform.DOLocalMove(lastLPos, t).OnComplete(() => boxCollider.enabled = true);
        }

        private void OnTriggerEnter2D(Collider2D _col)
        {
            if (!isDrager || SelectCheck(_col.transform)) return;

            DiceUI colDice = _col.GetComponent<DiceUI>();
            colDice.PosUpdate(this);
            
            transform.SetAsLastSibling();
        }
        #endregion
    }
}