using UnityEngine;
using UnityEngine.UI;

namespace SHY
{
    public class TurnShower : MonoBehaviour
    {
        private Image baseImg;
        private Image icon;

        private void Awake()
        {
            baseImg = GetComponent<Image>();
            icon = transform.Find("Icon").GetComponent<Image>();
        }

        public void UpdateImg(ShowerData _sd)
        {
            baseImg.color = _sd.backColor;
            icon.sprite = _sd.icon;
            gameObject.SetActive(true);
        }
    }

    public struct ShowerData
    {
        public Color backColor;
        public Sprite icon;

        public ShowerData(Color _color, Sprite _icon) 
        {
            backColor = _color;
            icon = _icon;
        }
    }
}