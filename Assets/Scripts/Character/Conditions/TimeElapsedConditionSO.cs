using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "TimeElapsedCondition", menuName = "State Machines/Conditions/Time Elapsed Condition")]
public class TimeElapsedConditionSO : StateConditionSO
{
    public float TimerDuration = 0.5f;
    protected override Condition CreateCondition() => new TimeElapsedCondition();
}

public class TimeElapsedCondition : Condition
{
    private float _startTime;
    protected new TimeElapsedConditionSO OriginSO => (TimeElapsedConditionSO)base.OriginSO;

    public override void Awake(StateMachine stateMachine)
    {
    }

    protected override bool Statement()
    {
        return Time.time >= _startTime + OriginSO.TimerDuration;
    }

    public override void OnStateEnter()
    {
        _startTime = Time.time;
    }

    public override void OnStateExit()
    {
    }
}
