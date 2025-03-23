
using karin.Charactor;
using UnityEngine;

namespace karin
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
        //����·�
        public int range;
        // ��������� �Ÿ����� ������ �ϴµ�
        public AttackType attackType;
        //�װ� �󸶳� ���ϰ�
        public int damage;
        //���� ȿ���� ���ϴ���
        public AttackEffect effect;
        //etc
        public BuffData buffData;
        public int additionalValue;

        public AttackData(
            Agent _who, Vector2Int _where, Direction _direction, int _range, AttackType _attackType
            , int _damage, AttackEffect _effect, int _additionalValue, BuffData _buffData = default)
        {
            who = _who;
            where = _where;
            direction = _direction;
            range = _range;
            attackType = _attackType;
            damage = _damage;
            effect = _effect;
            additionalValue = _additionalValue;
            buffData = _buffData;
        }
        public AttackData(
            Agent _who, Vector2Int _where, int _range, AttackType _attackType, int _damage
            , AttackEffect _effect = AttackEffect.None, int _additionalValue = 0, BuffData _buffData = default)
        {
            who = _who;
            where = _where;
            direction = _who.direction;
            range = _range;
            attackType = _attackType;
            damage = _damage;
            effect = _effect;
            additionalValue = _additionalValue;
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
        public MoveDirection moveDirection;
        //�󸶳� �̵��Ҳ���
        public int distance;
        //� ȿ���� ���ϴ���
        public MoveEffect effect;
        public int additionalValue;
        public bool rewriteStart;
        public bool rewriteEnd;

        public MoveData(Agent _who, Direction _direction, MoveDirection _moveDirection, MoveEffect _effect, int _distance, int _additionalValue, bool _rewriteStart = true, bool _rewriteEnd = true)
        {
            who = _who;
            direction = _direction;
            moveDirection = _moveDirection;
            effect = _effect;
            distance = _distance;
            additionalValue = _additionalValue;
            rewriteStart = _rewriteStart;
            rewriteEnd = _rewriteEnd;
        }
        public MoveData(Agent _who, int _distance, MoveDirection _moveDirection = MoveDirection.forward, MoveEffect _effect = MoveEffect.None, int _additionalValue = 0, bool _rewriteStart = true, bool _rewriteEnd = true)
        {
            who = _who;
            direction = _who.direction;
            moveDirection = _moveDirection;
            effect = _effect;
            distance = _distance;
            additionalValue = _additionalValue;
            rewriteStart = _rewriteStart;
            rewriteEnd = _rewriteEnd;
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