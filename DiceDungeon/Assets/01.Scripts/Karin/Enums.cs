
using System;

namespace karin
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
        Left = 0,
        TopLeft,
        TopRight,
        Right,
        BottomRight,
        BottomLeft,
    }

    public enum AttackEffect
    {
        None,
        EnchantBuff = 1,
    }

    public enum MoveEffect
    {
        None, //�ƹ��͵� �ƴ�
        Collision, //�浹
        //Repeat, //�� ������ ������ �ݴ����� ����
    }

    public enum Buff : int
    {
        Poison = 0,
        Strength
    }

    public enum ItemNames : int
    {
        Cockatrice = 0
    }


}