using Karin;
using Karin.Charactor;
using Karin.Event;
using SHY;
using UnityEngine;
using UnityEngine.UI;

public class DEBUGController : MonoBehaviour
{
    [SerializeField] private Button LeftMoveBtn;
    [SerializeField] private Button TopLeftMoveBtn;
    [SerializeField] private Button TopRightMoveBtn;
    [SerializeField] private Button RightMoveBtn;
    [SerializeField] private Button BotRightMoveBtn;
    [SerializeField] private Button BotLeftMoveBtn;

    [SerializeField] private AttackEyeSO attackSO;

    private void OnEnable()
    {
        LeftMoveBtn.onClick.AddListener(() => Move(Direction.Left));
        TopLeftMoveBtn.onClick.AddListener(() => Move(Direction.TopLeft));
        TopRightMoveBtn.onClick.AddListener(() => Move(Direction.TopRight));
        RightMoveBtn.onClick.AddListener(() => Move(Direction.Right));
        BotRightMoveBtn.onClick.AddListener(() => Move(Direction.BottomRight));
        BotLeftMoveBtn.onClick.AddListener(() => Move(Direction.BottomLeft));
    }

    private void OnDisable()
    {
        LeftMoveBtn.onClick    .RemoveListener(() => Move(Direction.Left));
        TopLeftMoveBtn.onClick .RemoveListener(() => Move(Direction.TopLeft));
        TopRightMoveBtn.onClick.RemoveListener(() => Move(Direction.TopRight));
        RightMoveBtn.onClick   .RemoveListener(() => Move(Direction.Right));
        BotRightMoveBtn.onClick.RemoveListener(() => Move(Direction.BottomRight));
        BotLeftMoveBtn.onClick .RemoveListener(() => Move(Direction.BottomLeft));
    }

    private void Move(Direction dir)
    {
        //나중에 매니저에서 가져오는걸로 교체
        Agent player = FindFirstObjectByType<Player>();
        //Agent player = BattleManager.Instance.player;
        MoveData md = new();
        md.who = player;
        md.direction = dir;
        md.distance = 1;
        md.effect = MoveEffect.None;
        EventManager.Instance.MoveEvent?.Invoke(md);
    }
    public void Attack()
    {
        //나중에 매니저에서 가져오는걸로 교체
        Agent player = FindFirstObjectByType<Player>();
        //Agent player = BattleManager.Instance.player;
        EventManager.Instance.AttackEvent?.Invoke(attackSO.GetData(player));
    }
}
