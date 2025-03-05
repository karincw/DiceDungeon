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
            seq.AppendInterval(1f);
            seq.Append(transform.DOMoveY(4, 0.8f));
            seq.AppendInterval(1.4f);
            seq.OnComplete(() => ShakeFin.Invoke());
        }
    }
}