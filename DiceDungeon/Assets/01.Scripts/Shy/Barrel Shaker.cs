using UnityEngine;
using DG.Tweening;

namespace SHY
{
    public class BarrelShaker : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Shake();
            }
        }

        private void Shake()
        {
            Sequence seq = DOTween.Sequence();

            seq.Append(transform.DOShakePosition(1.25f, new Vector3(120, 0), 10, 0, false, false));
        }
    }
}