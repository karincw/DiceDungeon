using UnityEngine;
using DG.Tweening;
using System;

namespace SHY
{
    public class StagePlayer : SingleTon<StagePlayer>
    {
        public Action<SceneType> fin;

        public void DoMove(Vector3 _pos)
        {
            Sequence sq = DOTween.Sequence();

            sq.Append(transform.DOMove(_pos, 0.5f).SetEase(Ease.OutBounce));

            //일정 시간 이후 게임매니저에 신호
            sq.AppendInterval(0.5f);

            sq.OnComplete(() => fin?.Invoke(SceneType.Battle));
        }
    }
}