
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
        penetration, // ����
        poison,
    }

    [Flags]
    public enum MoveEffect
    {
        penetration, // ����
        Collision, //�浹
    }

    public enum buffType
    {
        
    }



}