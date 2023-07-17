using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using Xiaohai.Character;

[CreateAssetMenu(fileName = "IsAttackingCondition", menuName = "State Machines/Conditions/Is Attacking Condition")]
public class IsAttackingConditionSO : StateConditionSO
{
    protected override Condition CreateCondition() => new IsAttackingCondition();
}

public class IsAttackingCondition : Condition
{
    private Character _character;
    protected new IsAttackingConditionSO OriginSO => (IsAttackingConditionSO)base.OriginSO;

    public override void Awake(StateMachine stateMachine)
    {
        _character = stateMachine.GetComponent<Character>();
    }

    protected override bool Statement()
    {
        return _character.AttackInput;
    }

    public override void OnStateEnter()
    {
    }

    public override void OnStateExit()
    {
    }
}
