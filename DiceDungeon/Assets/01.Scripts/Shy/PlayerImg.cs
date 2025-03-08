using UnityEngine;
using DG.Tweening;

public class PlayerImg : SingleTon<PlayerImg>
{
    public void DoMove(Vector3 _pos)
    {
        transform.DOMove(_pos, 0.5f);
    }
}
