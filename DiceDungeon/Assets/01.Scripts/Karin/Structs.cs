
using karin.Charactor;
using UnityEngine;

namespace karin
{
    [System.Serializable]
    public struct AttackData
    {
        //누가
        public Agent who;
        //어디서
        public Vector2Int where;
        //어디를 바라보고
        public Direction direction;
        //어떤형태로
        public int range;
        // 어느정도의 거리까지 공격을 하는데
        public AttackType attackType;
        //그게 얼마나 강하고
        public int damage;
        //무슨 효과를 지니는지
        public AttackEffect effect;
        //etc
        public BuffData buffData;

        public AttackData(Agent _who, Vector2Int _where, Direction _direction, int _range, AttackType _attackType, int _damage, AttackEffect _effect, BuffData _buffData = default)
        {
            who = _who;
            where = _where;
            direction = _direction;
            range = _range;
            attackType = _attackType;
            damage = _damage;
            effect = _effect;
            buffData = _buffData;
        }
    }

    [System.Serializable]
    public struct ShieldData
    {
        //누가
        public Agent who;
        //어느정도의 쉴드량을 얻는지
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
        //누가
        public Agent who;
        //어느방향으로
        public Direction direction;
        //얼마나 이동할꺼고
        public int distance;
        //어떤 효과를 지니는지
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
        //누가
        public Agent who;
        //어떤 버프를
        public Buff buffType;
        ///몇 턴 동안 지속할지 infinite : -1
        public int value;

        public BuffData(Agent _who, Buff _buffType, int _value)
        {
            who = _who;
            buffType = _buffType;
            value = _value;
        }
    }

}