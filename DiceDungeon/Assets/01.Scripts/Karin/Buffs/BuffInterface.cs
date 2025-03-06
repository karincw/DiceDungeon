using System.Collections.Generic;
using UnityEngine;

namespace Karin.BuffSystem
{
    public class BuffInterface : MonoBehaviour
    {
        private Dictionary<Sprite, Icon> _iconDictionary = new();
        [SerializeField] private Icon _iconPrefab;

        public void AddIcon(Sprite iconSprite, int value)
        {
            if (_iconDictionary.ContainsKey(iconSprite))
            {
                UpdateIcon(iconSprite, _iconDictionary[iconSprite].value + value);
                return;
            }
            var icon = Instantiate(_iconPrefab, transform);
            icon.countText.text = value.ToString();
            icon.iconImage.sprite = iconSprite;
            icon.value = value;
            _iconDictionary.Add(iconSprite, icon);
        }
        public void UpdateIcon(Sprite iconSprite, int value)
        {
            if (!_iconDictionary.ContainsKey(iconSprite))
                Debug.LogWarning("�������� �ʴ� ������ ������Ʈ��");

            _iconDictionary[iconSprite].countText.text = value.ToString();
            _iconDictionary[iconSprite].value = value;
        }
        public void RemoveIcon(Sprite iconSprite)
        {
            if (!_iconDictionary.ContainsKey(iconSprite))
                Debug.LogWarning($"{iconSprite} �������� �ʴ� ������ ������");

            Destroy(_iconDictionary[iconSprite].gameObject);
            _iconDictionary.Remove(iconSprite);
        }
    }
}
