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
    private MarcoPolo _polo;

    protected new AbilityTwoActionSO OriginSO => (AbilityTwoActionSO)base.OriginSO;

    public override void Awake(StateMachine stateMachine)
    {
        _character = stateMachine.GetComponent<Character>();
        _polo = stateMachine.GetComponent<MarcoPolo>();

    }

    public override void OnUpdate()
    {
    }

    public override void OnStateEnter()
    {
        _polo.PerformAbilityTwo();
        // consume the input, so that it can transition to other state
        _character.AbilityTwoInput = false;
    }



    public override void OnStateExit()
    {
    }
}
