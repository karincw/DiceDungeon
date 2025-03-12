using karin;
using karin.Charactor;
using karin.Event;
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
    [SerializeField] private ShieldEyeSO shieldSO;
    [SerializeField] private BuffEyeSO buffSO;
    [SerializeField] private MoveEyeSO moveSO;

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
        LeftMoveBtn.onClick.RemoveListener(() => Move(Direction.Left));
        TopLeftMoveBtn.onClick.RemoveListener(() => Move(Direction.TopLeft));
        TopRightMoveBtn.onClick.RemoveListener(() => Move(Direction.TopRight));
        RightMoveBtn.onClick.RemoveListener(() => Move(Direction.Right));
        BotRightMoveBtn.onClick.RemoveListener(() => Move(Direction.BottomRight));
        BotLeftMoveBtn.onClick.RemoveListener(() => Move(Direction.BottomLeft));
    }

    private void Move(Direction dir)
    {
        Agent player = BattleManager.Instance.player;
        MoveData md = new();
        md.who = player;
        md.direction = dir;
        md.distance = 1;
        md.effect = MoveEffect.None;
        EventManager.Instance.MoveEvent?.Invoke(md);
    }
    public void Attack()
    {
        if (attackSO == null) return;
        Agent player = BattleManager.Instance.player;
        EventManager.Instance.AttackEvent?.Invoke(attackSO.GetData(player));
    }
    public void Shield()
    {
        if (shieldSO == null) return;
        Agent player = BattleManager.Instance.player;
        EventManager.Instance.ShieldEvent?.Invoke(shieldSO.GetData(player));
    }
    public void Buff()
    {
        if (buffSO == null) return;
        Agent player = BattleManager.Instance.player;
        EventManager.Instance.BuffEvent?.Invoke(buffSO.GetData(player));
    }
    public void Move()
    {
        if (moveSO == null) return;
        Agent player = BattleManager.Instance.player;
        EventManager.Instance.MoveEvent?.Invoke(moveSO.GetData(player));
    }
}
