using karin;
using UnityEngine;
using karin.Charactor;

public class Player : Agent
{
    [SerializeField] private DirectionChanger _directionChanger;

    protected override void Awake()
    {
        base.Awake();
        _directionChanger = FindFirstObjectByType<DirectionChanger>();
    }

    public override void MoveStart(Direction dir, bool ReWrite = true)
    {
        base.MoveStart(dir, ReWrite);
        _directionChanger.SetDirection(dir);
    }

}
