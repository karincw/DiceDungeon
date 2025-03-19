using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace karin.Inventory
{

    public class SlotUI : MonoBehaviour
    {
        public ItemSO resource;

        public int maxCount = 0;

        public RectTransform IconRect
        {
            get
            {
                _iconRect = _icon.rectTransform;
                return _iconRect;
            }
            set
            {
                _iconRect = value;
            }
        }
        private RectTransform _iconRect;

        public Image Icon
        {
            get
            {
                if (_icon == null)
                    _icon = transform.Find("ItemImage").GetComponent<Image>();

                return _icon;
            }
            private set
            {
                _icon = value;
            }
        }
        private Image _icon;

        public Image Border
        {
            get
            {
                if (_border == null)
                    _border = transform.Find("Border").GetComponent<Image>();

                return _border;
            }
            private set
            {
                _border = value;
            }
        }
        private Image _border;

        public TextMeshProUGUI CountText
        {
            get
            {
                if (_countText == null)
                    _countText = transform.GetComponentInChildren<TextMeshProUGUI>();

                return _countText;
            }
            private set
            {
                _countText = value;
            }
        }
        private TextMeshProUGUI _countText;

        public bool HasItem => resource != null;

        private void OnEnable()
        {
            Refresh();
        }

        public virtual bool CanAdd(int count)
        {
            return maxCount >= resource.count + count && resource.maxCount >= resource.count + count;
        }
        public virtual bool CanRemove(int count)
        {
            return resource.count - count >= 0;
        }
        public virtual int MaxAdd()
        {
            if (resource != null)
            {
                return Mathf.Min(maxCount - resource.count, resource.maxCount - resource.count);
            }
            else
                return maxCount;
        }
        public virtual void Refresh()
        {
            if (resource == null || resource.count == 0)
            {
                resource = null;
                Icon.sprite = null;
                Icon.color = Color.clear;
                CountText.text = string.Empty;

                return;
            }
            Icon.color = Color.white;
            Icon.sprite = resource.image;
            CountText.text = resource.count.ToString();
        }
        public virtual void EnableBorder(bool state)
        {
            Border.gameObject.SetActive(state);
        }
    }
}