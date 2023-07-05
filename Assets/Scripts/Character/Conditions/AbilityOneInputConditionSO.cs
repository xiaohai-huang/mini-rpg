using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "AbilityOneInputCondition", menuName = "State Machines/Conditions/Ability One Input Condition")]
public class AbilityOneInputConditionSO : StateConditionSO
{
    protected override Condition CreateCondition() => new AbilityOneInputCondition();
}

public class AbilityOneInputCondition : Condition
{
    private Character _character;
    protected new AbilityOneInputConditionSO OriginSO => (AbilityOneInputConditionSO)base.OriginSO;

    public override void Awake(StateMachine stateMachine)
    {
        _character = stateMachine.GetComponent<Character>();
    }

    protected override bool Statement()
    {
        return _character.AbilityOneInput;
    }

    public override void OnStateEnter()
    {
    }

    public override void OnStateExit()
    {
    }
}
