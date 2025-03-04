
using UnityEngine;

namespace Karin
{
    [System.Serializable]
    public struct AttackData
    {
        //����
        public GameObject who;
        //���
        public Transform where;
        //��� �ٶ󺸰�
        public Direction direction;
        //����·� ������ �ϴµ�
        public AttackType attackType;
        //�װ� �󸶳� ���ϰ�
        public int damage;
        //���� ȿ���� ���ϴ���
        public AttackEffect effect;
    }

    [System.Serializable]
    public struct ShieldData
    {
        //����
        public GameObject who;
        //��������� ���差�� �����
        public int shield;
    }

    [System.Serializable]
    public struct MoveData
    {
        //����
        public GameObject who;
        //�����������
        public Direction direction;
        //�󸶳� �̵��Ҳ���
        public int distance;
        //� ȿ���� ���ϴ���
        public MoveEffect effect;
    }

    [System.Serializable]
    public struct BuffData
    {
        //����
        public GameObject who;
        //� ������
        public buffType buffType;
        ///�� �� ���� �������� infinite : -1
        public int time;
    }


}