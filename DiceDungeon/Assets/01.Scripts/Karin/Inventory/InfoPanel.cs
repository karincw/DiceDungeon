using DG.Tweening;
using TMPro;
using UnityEngine;

namespace karin.Inventory
{
    public class InfoPanel : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;
        private TMP_Text _nameText;
        private TMP_Text _descriptionText;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _nameText = transform.Find("TopBar").Find("DiceName").GetComponent<TMP_Text>();
            _descriptionText = transform.Find("Description").GetComponent<TMP_Text>();

            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }

        private bool _enabled = false;

        public void ShowInfo(ShowInfoData infoData, Vector2 clickedPos)
        {
            if (_enabled)
            {
                _canvasGroup.DOFade(0, 0.15f)
                    .OnComplete(() =>
                    {
                        transform.position = clickedPos;
                        _canvasGroup.DOFade(1, 0.3f).OnComplete(() => {
                            _canvasGroup.interactable = true;
                            _canvasGroup.blocksRaycasts = true;
                        });
                        _nameText.text = infoData.infoName;
                        _descriptionText.text = infoData.infoDescription;
                    });
            }
            transform.position = clickedPos;
            _enabled = true;
            _nameText.text = infoData.infoName;
            _descriptionText.text = infoData.infoDescription;
            _canvasGroup.DOFade(1, 0.3f).OnComplete(() => { 
                _canvasGroup.interactable = true;
                _canvasGroup.blocksRaycasts = true;
            });
        }

        public void CloseInfo()
        {
            _enabled = false;
            _canvasGroup.DOFade(0, 0.15f)
                .OnComplete(() =>
                {
                    _canvasGroup.interactable = false;
                    _canvasGroup.blocksRaycasts = false;
                    transform.position = Vector2.zero;
                });
        }

    }
}