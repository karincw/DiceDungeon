using Karin;
using Karin.Charactor;
using Karin.Event;
using UnityEditor.Overlays;
using UnityEngine;

public class Player : Agent
{
    [SerializeField] AttackType type;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveData moveData = new MoveData();
            moveData.who = this;
            moveData.direction = Direction.Left;
            moveData.distance = 1;
            EventManager.Instance.MoveEvent?.Invoke(moveData);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            MoveData moveData = new MoveData();
            moveData.who = this;
            moveData.direction = Direction.TopLeft;
            moveData.distance = 1;
            EventManager.Instance.MoveEvent?.Invoke(moveData);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            MoveData moveData = new MoveData();
            moveData.who = this;
            moveData.direction = Direction.TopRight;
            moveData.distance = 1;
            EventManager.Instance.MoveEvent?.Invoke(moveData);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            MoveData moveData = new MoveData();
            moveData.who = this;
            moveData.direction = Direction.Right;
            moveData.distance = 1;
            EventManager.Instance.MoveEvent?.Invoke(moveData);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            MoveData moveData = new MoveData();
            moveData.who = this;
            moveData.direction = Direction.BottomRight;
            moveData.distance = 1;
            EventManager.Instance.MoveEvent?.Invoke(moveData);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            MoveData moveData = new MoveData();
            moveData.who = this;
            moveData.direction = Direction.BottomLeft;
            moveData.distance = 1;
            EventManager.Instance.MoveEvent?.Invoke(moveData);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            MoveData moveData = new MoveData();
            moveData.who = this;
            moveData.direction = Direction.BottomLeft;
            moveData.distance = 1;
            EventManager.Instance.MoveEvent?.Invoke(moveData);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            AttackData attackData = new AttackData();
            attackData.who = this;
            attackData.direction = direction;
            attackData.damage = 5;
            attackData.attackType = type;
            EventManager.Instance.AttackEvent?.Invoke(attackData);
        }
    }
}
