using FSM;
using UnityEngine;

public class AttackState : State
{
    public AttackState() : base(needsExitTime: false)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("Perform the attack");
    }

    public override void OnLogic()
    {
        if (timer.Elapsed > 0.2f)
        {
            fsm.RequestStateChange("Idle");
        }
    }
}
