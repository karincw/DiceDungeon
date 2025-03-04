
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

    public enum Direction
    {
        Left,
        Right,
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight
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

    public enum buffType
    {
        
    }



}