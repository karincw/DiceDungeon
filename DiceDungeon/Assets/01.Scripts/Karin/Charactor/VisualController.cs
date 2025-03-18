using UnityEngine;

namespace karin.Charactor
{
    public class VisualController : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private Agent _owner;
        [SerializeField] private bool _viewRight;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Init(Agent owner)
        {
            _owner = owner;
        }

        public void UpdateViewDirection(Direction direction)
        {
            if (2 <= (int)direction && (int)direction <= 4)
            {
                if (_viewRight) return;

                _viewRight = !_viewRight;
                _spriteRenderer.flipX = !_viewRight;
            }
            else
            {
                if (!_viewRight) return;

                _viewRight = !_viewRight;
                _spriteRenderer.flipX = !_viewRight;
            }
        }
    }
}
