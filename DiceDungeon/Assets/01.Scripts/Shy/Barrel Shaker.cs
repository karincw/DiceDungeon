using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

namespace SHY
{
    public class BarrelShaker : MonoBehaviour, IDragHandler
    {
        private enum ShakeType
        {
            TopView,
            SideView,
            Self
        }

        [SerializeField] private ShakeType type;

        private void Start()
        {
            if (type != ShakeType.Self)
            Shake();
        }

        public void Shake()
        {
            transform.DOMoveX(0.1f, 0.3f);

        }

        public void OnDrag(PointerEventData eventData)
        {
            Debug.Log(eventData.position);
            transform.position = Camera.main.ScreenToWorldPoint(eventData.position);
        }
    }
}
