using UnityEngine;
using FSM;
public class WalkState : StateBase
{
    readonly PlayerController _mono;
    public WalkState(PlayerController mono) : base(needsExitTime: false) { _mono = mono; }

    public override void OnEnter()
    {
        base.OnEnter();
        _mono.CharacterMovement.SetDestination(InputUtil.GetPointerPosition());
    }

    public override void OnLogic()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _mono.CharacterMovement.SetDestination(InputUtil.GetPointerPosition());
        }
    }

    public override void OnExit()
    {
        Debug.Log("Finish walk");
    }

   
}
