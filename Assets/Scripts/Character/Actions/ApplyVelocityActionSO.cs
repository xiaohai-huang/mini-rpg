﻿using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "ApplyVelocityAction", menuName = "State Machines/Actions/Apply Velocity Action")]
public class ApplyVelocityActionSO : StateActionSO
{
    protected override StateAction CreateAction() => new ApplyVelocityAction();
}

public class ApplyVelocityAction : StateAction
{
    private CharacterController _characterController;
    private Character _character;
    protected new ApplyVelocityActionSO OriginSO => (ApplyVelocityActionSO)base.OriginSO;

    public override void Awake(StateMachine stateMachine)
    {
        _characterController = stateMachine.GetComponent<CharacterController>();
        _character = stateMachine.GetComponent<Character>();
    }

    public override void OnUpdate()
    {
        _characterController.Move(_character.Velocity * Time.deltaTime);
        _character.Velocity = _characterController.velocity;
    }

    public override void OnStateEnter()
    {
    }

    public override void OnStateExit()
    {
    }
}