using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using Xiaohai.Character;

[CreateAssetMenu(fileName = "IsDeadCondition", menuName = "State Machines/Conditions/Is Dead Condition")]
public class IsDeadConditionSO : StateConditionSO
{
    protected override Condition CreateCondition() => new IsDeadCondition();
}

public class IsDeadCondition : Condition
{
    protected new IsDeadConditionSO OriginSO => (IsDeadConditionSO)base.OriginSO;
    private Damageable _damageable;
    public override void Awake(StateMachine stateMachine)
    {
        _damageable = stateMachine.GetComponent<Damageable>();
    }

    protected override bool Statement()
    {
        return _damageable.IsDead;
    }

    public override void OnStateEnter()
    {
    }

    public override void OnStateExit()
    {
    }
}
