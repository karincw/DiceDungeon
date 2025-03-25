using karin.Charactor;
using karin.HexMap;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace karin.Charactor
{
    public class AttackEffector : MonoBehaviour
    {
        private Agent _owner;
        private Animator _animator;

        public void Init(Agent owner)
        {
            _owner = owner;
            _animator = GetComponent<Animator>();
        }

        public void PlayAttackEffect(Direction attackDirection, int attackDistance, AttackType attackType)
        {
            var dirVector = HexCoordinates.GetDirectionToVector(attackDirection);
            transform.localPosition = dirVector.normalized * attackDistance;
            transform.localScale = Vector3.one + Vector3.one * (0.5f * attackDistance - 1);
            
            Vector2 viewVector = HexCoordinates.GetDirectionToVector(_owner.direction);
            float rotZ = Mathf.Atan2(viewVector.y - transform.position.y, viewVector.x - transform.position.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rotZ);

            switch (attackType)
            {
                case AttackType.Around:
                    transform.localPosition = Vector3.zero;
                    transform.localScale = Vector3.one + Vector3.one * (attackDistance - 1 + attackDistance == 3 ? 1 : 0);
                    transform.Rotate(Vector3.forward, 180);
                    break;
                case AttackType.Front:
                    transform.Rotate(Vector3.forward, 20);
                    break;
                case AttackType.Fan:
                    transform.Rotate(Vector3.forward, 180);
                    break;
                case AttackType.AllAround:
                    transform.localPosition = Vector3.zero;
                    transform.localScale = Vector3.one + Vector3.one * (attackDistance - 1 + attackDistance == 3 ? 1 : 0);
                    transform.Rotate(Vector3.forward, 180);
                    break;
                default:
                    break;
            }

            _animator.SetTrigger(attackType.ToString());
        }
    }
}