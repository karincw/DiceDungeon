using System;

namespace karin
{
    public enum AttackType
    {
        Around,
        Front,
        Fan,
        AllAround,
    }

    public enum Direction : int
    {
        Left = 0,
        TopLeft,
        TopRight,
        Right,
        BottomRight,
        BottomLeft,
    }

    public enum MoveDirection : int
    {
        forward,
        backward
    }

    [Flags]
    public enum AttackEffect
    {
        None = 0,
        EnchantBuff = 1,
        procedural = 2, //가장 앞의 적만 공격
    }

    public enum MoveEffect
    {
        None, //아무것도 아님
        Collision, //충돌
        //Repeat, //맵 밖으로 나가면 반대편에서 나옴
    }

    public enum Buff : int
    {
        Poison = 0,
        Strength
    }

    public enum DiceEffect
    {
        None,
        AddAttack,
        AddShield,
    }
}