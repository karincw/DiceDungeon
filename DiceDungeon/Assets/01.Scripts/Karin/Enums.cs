
using System;

namespace Karin
{
    public enum AttackType
    {
        Around,
        Front,
        Fan,
        AllAround
    }

    public enum Direction : int
    {
        Left,
        TopLeft,
        TopRight,
        Right,
        BottomRight,
        BottomLeft,
    }

    [Flags]
    public enum AttackEffect
    {
        penetration, // ����
        poison,
    }

    [Flags]
    public enum MoveEffect
    {
        penetration, // ����
        Collision, //�浹
    }

    public enum Buff : int
    {
        Poison = 0
    }



}