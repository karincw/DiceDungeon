
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
        penetration, // 관통
        poison,
    }

    [Flags]
    public enum MoveEffect
    {
        penetration, // 관통
        Collision, //충돌
    }

    public enum Buff : int
    {
        Poison = 0
    }



}