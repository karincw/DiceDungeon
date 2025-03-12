
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
        None,
        EnchantBuff = 1,
    }

    [Flags]
    public enum MoveEffect
    {
        None, //아무것도 아님
        penetration, // 관통
        Collision, //충돌
    }

    public enum Buff : int
    {
        Poison = 0,
        Strength
    }



}