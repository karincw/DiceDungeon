
using Karin.Charactor;
using Karin.BuffSystem;
using UnityEngine;

namespace Karin
{
    [System.Serializable]
    public struct AttackData
    {
        //����
        public Agent who;
        //���
        public Vector2Int where;
        //��� �ٶ󺸰�
        public Direction direction;
        //����·� ������ �ϴµ�
        public AttackType attackType;
        //�װ� �󸶳� ���ϰ�
        public int damage;
        //���� ȿ���� ���ϴ���
        public AttackEffect effect;
        public BuffData buffData;
    }

    [System.Serializable]
    public struct ShieldData
    {
        //����
        public Agent who;
        //��������� ���差�� �����
        public int shield;
    }

    [System.Serializable]
    public struct MoveData
    {
        //����
        public Agent who;
        //�����������
        public Direction direction;
        //�󸶳� �̵��Ҳ���
        public int distance;
        //� ȿ���� ���ϴ���
        public MoveEffect effect;
        public int additionalValue;
    }

    [System.Serializable]
    public struct BuffData
    {
        //����
        public Agent who;
        //� ������
        public Buff buffType;
        ///�� �� ���� �������� infinite : -1
        public int value;
    }

}