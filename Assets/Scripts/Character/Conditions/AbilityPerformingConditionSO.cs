using Core.Game.Combat;
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
    public Character.Ability Type;

    protected override Condition CreateCondition() => new AbilityPerformingCondition();
}

public class AbilityPerformingCondition : Condition
{
    protected new AbilityPerformingConditionSO OriginSO =>
        (AbilityPerformingConditionSO)base.OriginSO;
    private AbilityBase _ability;

    public override void Awake(StateMachine stateMachine)
    {
        _ability = AbilityBase.GetAbility(stateMachine, OriginSO.Type);
    }

    protected override bool Statement()
    {
        return _ability.Performing;
    }
}
