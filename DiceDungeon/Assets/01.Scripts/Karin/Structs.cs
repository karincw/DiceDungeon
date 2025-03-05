
using UnityEngine;
using Karin.Charactor;

namespace Karin
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
        //어떤형태로 공격을 하는데
        public AttackType attackType;
        //그게 얼마나 강하고
        public int damage;
        //무슨 효과를 지니는지
        public AttackEffect effect;
    }

    [System.Serializable]
    public struct ShieldData
    {
        //누가
        public Agent who;
        //어느정도의 쉴드량을 얻는지
        public int shield;
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
    }

    [System.Serializable]
    public struct BuffData
    {
        //누가
        public Agent who;
        //어떤 버프를
        public buffType buffType;
        ///몇 턴 동안 지속할지 infinite : -1
        public int time;
    }


}