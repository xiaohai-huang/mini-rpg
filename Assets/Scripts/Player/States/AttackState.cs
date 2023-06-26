using FSM;
using UnityEngine;

public class AttackState : State
{
    private readonly PlayerController _mono;
    private readonly AttackHandler _attackHandler;
    public AttackState(PlayerController mono) : base(needsExitTime: false)
    {
        _mono = mono;
        _attackHandler = mono.GetComponent<AttackHandler>();
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _attackHandler.Attack();
    }

    public override void OnLogic()
    {
        if (timer.Elapsed > 0.2f)
        {
            fsm.RequestStateChange("Idle");
        }
    }
}
