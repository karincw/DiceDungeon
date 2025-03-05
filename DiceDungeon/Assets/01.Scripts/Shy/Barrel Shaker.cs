using UnityEngine;
using DG.Tweening;
using System;

namespace SHY
{
    public class BarrelShaker : MonoBehaviour
    {
        public Action ShakeFin;

        public void Shake()
        {
            Sequence seq = DOTween.Sequence();

            seq.Append(transform.DOShakePosition(1.25f, new Vector3(120, 0), 10, 0, false, false));
            seq.OnComplete(() => ShakeFin.Invoke());
        }
    }
}