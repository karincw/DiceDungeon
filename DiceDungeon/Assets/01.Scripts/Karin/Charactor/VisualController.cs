using UnityEngine;

namespace karin.Charactor
{
    public class VisualController : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private Animator _animator;
        private Agent _owner;
        [SerializeField] private bool _viewRight;

        private int _animtaionIdleHash = Animator.StringToHash("Idle");

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
        }

        public void Init(Agent owner)
        {
            _owner = owner;
            EndAnimation();
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

        public void PlayAnimation(string animationName)
        {
            _animator.SetTrigger(animationName);
        }
        public void EndAnimation()
        {
            _animator.SetTrigger(_animtaionIdleHash);
        }
    }
}
