using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using Xiaohai.Character;

[CreateAssetMenu(
    fileName = "IsMovingCondition",
    menuName = "State Machines/Conditions/Is Moving Condition"
)]
public class IsMovingConditionSO : StateConditionSO
{
    public float Threshold = 0.02f;

    protected override Condition CreateCondition() => new IsMovingCondition();
}

public class IsMovingCondition : Condition
{
    private Character _character;
    protected new IsMovingConditionSO OriginSO => (IsMovingConditionSO)base.OriginSO;

    public override void Awake(StateMachine stateMachine)
    {
        _character = stateMachine.GetComponent<Character>();
    }

    protected override bool Statement()
    {
        return _character.HorizontalInput.sqrMagnitude > OriginSO.Threshold;
    }

    public override void OnStateEnter() { }

    public override void OnStateExit() { }
}
