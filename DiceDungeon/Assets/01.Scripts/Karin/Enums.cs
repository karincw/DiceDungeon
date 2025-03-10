
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
        penetration, // 관통
        poison,
    }

    [Flags]
    public enum MoveEffect
    {
        None, //아무것도 아님
        penetration, // 관통
        Collision, //충돌
        Push, //밀치기
    }

    public enum Buff : int
    {
        Poison = 0
    }



}