using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using Xiaohai.Character;

[CreateAssetMenu(fileName = "StopMoveAction", menuName = "State Machines/Actions/Stop Move Action")]
public class StopMoveActionSO : StateActionSO
{
    protected override StateAction CreateAction() => new StopMoveAction();
}

public class StopMoveAction : StateAction
{
    private Character _character;
    protected new StopMoveActionSO OriginSO => (StopMoveActionSO)base.OriginSO;

    public override void Awake(StateMachine stateMachine)
    {
        _character = stateMachine.GetComponent<Character>();
    }

    public override void OnUpdate()
    {
        _character.Velocity = Vector3.zero;
    }

    public override void OnStateEnter()
    {
    }

    public override void OnStateExit()
    {
    }
}
