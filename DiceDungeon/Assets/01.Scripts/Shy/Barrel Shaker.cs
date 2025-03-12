using UnityEngine;
using DG.Tweening;
using System;

namespace SHY
{
    public class BarrelShaker : MonoBehaviour
    {
        public Action shakeFin;
        public Action openCup;

        private Transform visual;

        private void Awake()
        {
            visual = transform.GetChild(0);
        }

        public void Shake()
        {
            Sequence seq = DOTween.Sequence();

            seq.SetDelay(0.7f);

            //Down
            seq.Append(visual.DOLocalMoveY(0, 1f).SetEase(Ease.OutCubic));
            seq.AppendInterval(0.8f);

            //Shake
            seq.Append(visual.DOShakePosition(1.25f, new Vector3(120, 0), 10, 0, false, false));
            seq.AppendInterval(1f);

            //Up
            seq.Append(visual.DOMoveY(5, 0.8f).OnStart(() => openCup?.Invoke()));
            seq.AppendInterval(1.1f);
            seq.OnComplete(() => shakeFin.Invoke());
        }
        
        public void Disappear(float _sp = 0.3f, float _de = 0.2f)
        {
            Sequence seq = DOTween.Sequence();

            seq.Append(visual.DOLocalMoveY(1080, _sp));
            seq.SetDelay(_de);
        }
    }
}