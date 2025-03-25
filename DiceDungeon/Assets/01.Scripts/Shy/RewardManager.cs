using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using karin.Inventory;
using System.Collections.Generic;
using DG.Tweening;

namespace SHY.Reward
{
    public class RewardManager : SingleTon<RewardManager>
    {
        private struct RewardCell
        {
            public EyeSO data;
            public Image itemImg;
            public TextMeshProUGUI itemName;
            public TextMeshProUGUI itemExplain;
        }

        [SerializeField] private Transform[] itemPos = new Transform[4];
        RewardCell[] items = new RewardCell[4];

        [SerializeField] private GameObject notUI;
        [SerializeField] private CanvasGroup battleScene;
        [SerializeField] private CanvasGroup rewardUI;

        [SerializeField] private UIOpener opener;
        [SerializeField] private StageManager stageManager;

        private void Awake()
        {
            if (stageManager == null) FindFirstObjectByType<StageManager>();

            for (int i = 0; i < items.Length; i++)
            {
                items[i].itemImg = itemPos[i].Find("Icon").GetChild(0).GetComponent<Image>();
                items[i].itemName = itemPos[i].Find("Name").GetComponent<TextMeshProUGUI>();
                items[i].itemExplain = itemPos[i].Find("Explain").GetComponent<TextMeshProUGUI>();
            }

            rewardUI.alpha = 0;
        }

        public void AddItem(int _num)
        {
            ItemSO _item = new ItemSO();
            _item.itemName = items[_num].data.itemName;
            _item.image = items[_num].data.icon;
            Inventory.Instance.AddItem(_item);
            BattleSceneFin();
        }

        public void OnPlay()
        {
            Debug.Log("Reward UI");

            rewardUI.gameObject.SetActive(true);
            rewardUI.alpha = 0;

            //Data를 가져오는 코드
            List<EyeSO> rewards = stageManager.nowChapter.GetRewards();

            for (int i = 0; i < items.Length; i++)
            {
                if(rewards.Count <= i)
                {
                    itemPos[i].gameObject.SetActive(false);
                    continue;
                }

                itemPos[i].gameObject.SetActive(true);
                items[i].data = rewards[i];
                items[i].itemName.text = rewards[i].eyeName;
                items[i].itemExplain.text = rewards[i].eyeName;
                items[i].itemImg.sprite = rewards[i].icon;
            }

            CanvasAlpha(rewardUI, true);
        }

        private void CanvasAlpha(CanvasGroup _group, bool isOpen)
        {
            Sequence seq = DOTween.Sequence();

            if (!isOpen)
            {
                GameManager.Instance.SceneChange(SceneType.Stage);
                notUI.SetActive(false);
                seq.OnComplete(() => 
                {
                    notUI.SetActive(true);
                    _group.alpha = 1;
                    rewardUI.alpha = 0;
                });
            }
            seq.Append(_group.DOFade(isOpen ? 1 : 0, 0.5f)).OnStart(()=>opener.ActiveSwitch());
        }

        public void BattleSceneFin()
        {
            CanvasAlpha(battleScene, false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P)) OnPlay();
            if (Input.GetKeyDown(KeyCode.O)) BattleSceneFin();
        }
    }
}