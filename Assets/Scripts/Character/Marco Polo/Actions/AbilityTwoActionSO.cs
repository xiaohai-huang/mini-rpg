using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "AbilityTwoAction", menuName = "State Machines/Actions/Marco Polo/Ability Two Action")]
public class AbilityTwoActionSO : StateActionSO
{
    public float Distance = 3f;
    protected override StateAction CreateAction() => new AbilityTwoAction();
}

public class AbilityTwoAction : StateAction
{
    private Character _character;
    protected new AbilityTwoActionSO OriginSO => (AbilityTwoActionSO)base.OriginSO;

    public override void Awake(StateMachine stateMachine)
    {
        _character = stateMachine.GetComponent<Character>();
    }

    public override void OnUpdate()
    {
    }

    public override void OnStateEnter()
    {
        var direction = new Vector3(_character.AbilityTwoDirection.x, 0, _character.AbilityTwoDirection.y);
        if (direction == Vector3.zero)
        {
            _character.transform.position = _character.transform.position + _character.transform.forward * OriginSO.Distance;
        }
        else
        {
            _character.transform.position = _character.transform.position + (direction * OriginSO.Distance);
        }
    }



    public override void OnStateExit()
    {
        _character.AbilityTwoInput = false;
    }
}
