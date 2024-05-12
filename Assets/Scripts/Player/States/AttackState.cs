using UnityHFSM;

public class AttackState : State
{
    private readonly PlayerController _mono;
    private readonly AttackHandler _attackHandler;
    private StateMachine _sm;
    public AttackState(PlayerController mono) : base(needsExitTime: false)
    {
        _mono = mono;
        _attackHandler = mono.GetComponent<AttackHandler>();
        _sm = (StateMachine)fsm;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _attackHandler.Attack();
    }

    public override void OnLogic()
    {
        if (timer.Elapsed > 0.1f)
        {
            _sm.RequestStateChange("Idle");
        }
    }
}
