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

            //Down
            seq.Append(visual.DOLocalMoveY(0, 1f).OnComplete(()=>openCup?.Invoke()));
            seq.AppendInterval(0.8f);

            //Shake
            seq.Append(visual.DOShakePosition(1.25f, new Vector3(120, 0), 10, 0, false, false));
            seq.AppendInterval(1f);

            //Up
            seq.Append(visual.DOMoveY(4, 0.8f));
            seq.AppendInterval(1.4f);
            seq.OnComplete(() => shakeFin.Invoke());
        }
        
        public void Disappear(float _sp = 0.3f, float _de = 0.33f)
        {
            Sequence seq = DOTween.Sequence();

            seq.Append(visual.DOMoveY(10, _sp));
            seq.SetDelay(_de);
        }
    }
}