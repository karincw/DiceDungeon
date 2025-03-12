
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

        public AttackData(Agent _who, Vector2Int _where, Direction _direction, AttackType _attackType, int _damage, AttackEffect _effect, BuffData _buffData = default)
        {
            who = _who;
            where = _where;
            direction = _direction;
            attackType = _attackType;
            damage = _damage;
            effect = _effect;
            buffData = _buffData;
        }
    }

    [System.Serializable]
    public struct ShieldData
    {
        //����
        public Agent who;
        //��������� ���差�� �����
        public int value;

        public ShieldData(Agent _who, int _value)
        {
            who = _who;
            value = _value;
        }
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
        public bool rewriteTile;

        public MoveData(Agent _who, Direction _direction, MoveEffect _effect, int _distance, int _additionalValue, bool _reWriteTile = true)
        {
            who = _who;
            direction = _direction;
            effect = _effect;
            distance = _distance;
            additionalValue = _additionalValue;
            rewriteTile = _reWriteTile;
        }
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

        public BuffData(Agent _who, Buff _buffType, int _value)
        {
            who = _who;
            buffType = _buffType;
            value = _value;
        }
    }

}