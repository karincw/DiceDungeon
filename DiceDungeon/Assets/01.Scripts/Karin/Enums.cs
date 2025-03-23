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
        HeadButt, //�浹
    }

    [Flags]
    public enum MoveEffect
    {
        None, //�ƹ��͵� �ƴ�
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