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
        }

        public void Push(Color _color)
        {
            baseImg.color = _color;
            gameObject.SetActive(true);
        }
    }

    public struct ShowerData
    {
        Color backColor;
        Sprite icon;

        //public ShowerData() {}
    }
}