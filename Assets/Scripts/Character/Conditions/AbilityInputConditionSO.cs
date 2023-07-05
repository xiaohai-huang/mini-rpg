using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "AbilityInputCondition", menuName = "State Machines/Conditions/Ability Input Condition")]
public class AbilityInputConditionSO : StateConditionSO
{
    public Character.Ability Ability;
    protected override Condition CreateCondition() => new AbilityInputCondition();
}

public class AbilityInputCondition : Condition
{
    private Character _character;
    protected new AbilityInputConditionSO OriginSO => (AbilityInputConditionSO)base.OriginSO;

    public override void Awake(StateMachine stateMachine)
    {
        _character = stateMachine.GetComponent<Character>();
    }

    protected override bool Statement()
    {
        return _character.HasAbilityInput(OriginSO.Ability);
    }

    public override void OnStateEnter()
    {
    }

    public override void OnStateExit()
    {
    }
}
