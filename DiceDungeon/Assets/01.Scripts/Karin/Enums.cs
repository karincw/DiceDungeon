
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
        None, //�ƹ��͵� �ƴ�
        penetration, // ����
        Collision, //�浹
    }

    public enum Buff : int
    {
        Poison = 0,
        Strength
    }



}