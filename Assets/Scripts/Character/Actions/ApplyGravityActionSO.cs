using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using Xiaohai.Character;

[CreateAssetMenu(fileName = "ApplyGravityAction", menuName = "State Machines/Actions/Apply Gravity Action")]
public class ApplyGravityActionSO : StateActionSO
{
    public float Gravity = -9.81f;
    protected override StateAction CreateAction() => new ApplyGravityAction();
}

public class ApplyGravityAction : StateAction
{
    private Character _character;
    protected new ApplyGravityActionSO OriginSO => (ApplyGravityActionSO)base.OriginSO;

    public override void Awake(StateMachine stateMachine)
    {
        _character = stateMachine.GetComponent<Character>();
    }

    public override void OnUpdate()
    {
        _character.Velocity.y = OriginSO.Gravity;
    }

    public override void OnStateEnter()
    {
    }

    public override void OnStateExit()
    {
    }
}
