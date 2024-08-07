using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using Xiaohai.Character;

[CreateAssetMenu(
    fileName = "AbilityPerformingCondition",
    menuName = "State Machines/Conditions/Ability Performing Condition"
)]
public class AbilityPerformingConditionSO : StateConditionSO
{
    public Character.Ability Ability;

    protected override Condition CreateCondition() => new AbilityPerformingCondition();
}

public class AbilityPerformingCondition : Condition
{
    protected new AbilityPerformingConditionSO OriginSO =>
        (AbilityPerformingConditionSO)base.OriginSO;
    private Character _character;

    public override void Awake(StateMachine stateMachine)
    {
        _character = stateMachine.GetComponent<Character>();
    }

    protected override bool Statement()
    {
        return _character.IsAbilityPerforming(OriginSO.Ability);
    }

    public override void OnStateEnter() { }

    public override void OnStateExit() { }
}
