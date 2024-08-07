using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(
    fileName = "Contains Effect Condition",
    menuName = "State Machines/Conditions/Contains Effect Condition"
)]
public class ContainsEffectConditionSO : StateConditionSO
{
    public EffectSO EffectSO;

    protected override Condition CreateCondition() => new ContainsEffectCondition();
}

public class ContainsEffectCondition : Condition
{
    private EffectSystem _effectSystem;
    protected new ContainsEffectConditionSO OriginSO => (ContainsEffectConditionSO)base.OriginSO;

    public override void Awake(StateMachine stateMachine)
    {
        _effectSystem = stateMachine.GetComponent<EffectSystem>();
    }

    protected override bool Statement()
    {
        return _effectSystem.Contains(OriginSO.EffectSO);
    }

    public override void OnStateEnter() { }

    public override void OnStateExit() { }
}
