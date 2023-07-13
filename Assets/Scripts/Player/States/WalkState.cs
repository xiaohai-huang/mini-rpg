using FSM;
using UnityEngine;
public class WalkState : StateBase
{
    readonly PlayerController _mono;
    CharacterController _characterController;
    public WalkState(PlayerController mono) : base(needsExitTime: false) { _mono = mono; }

    public override void OnEnter()
    {
        base.OnEnter();
        _characterController = _mono.GetComponent<CharacterController>();
    }

    public override void OnLogic()
    {
        Vector2 moveInput = _mono.InputReader.Move;
        Vector3 direction = new Vector3(moveInput.x, 0, moveInput.y);
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        _mono.transform.rotation = Quaternion.Slerp(_mono.transform.rotation, targetRotation, _mono.RotateSpeed * Time.deltaTime);
        _characterController.Move(_mono.WalkSpeed * Time.deltaTime * _mono.transform.forward);
    }

    public override void OnExit()
    {
    }
}
