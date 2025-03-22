using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardSystem : MonoBehaviour
{
    private struct RewardCell
    {
        public Image itemImg;
        public TextMeshProUGUI itemName;
        public TextMeshProUGUI itemExplain;
    }

    [SerializeField] private Transform[] itemPos = new Transform[4];
    RewardCell[] items = new RewardCell[4];

    private void Awake()
    {
        for (int i = 0; i < 4; i++)
        {
            items[i].itemImg = itemPos[i].Find("Icon").GetComponent<Image>();
            items[i].itemName = itemPos[i].Find("Name").GetComponent<TextMeshProUGUI>();
            items[i].itemExplain = itemPos[i].Find("Explain").GetComponent<TextMeshProUGUI>();
        }
    }

    public void OnPlay()
    {
        Debug.Log("Reward UI");
        gameObject.SetActive(true);

    }
}
