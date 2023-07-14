using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "AbilityThreeAction", menuName = "State Machines/Actions/Marco Polo/Ability Three Action")]
public class AbilityThreeActionSO : StateActionSO
{
    protected override StateAction CreateAction() => new AbilityThreeAction();
}

public class AbilityThreeAction : StateAction
{
    private Character _character;
    private MarcoPolo _polo;
    protected new AbilityThreeActionSO OriginSO => (AbilityThreeActionSO)base.OriginSO;

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

        _polo.PerformAbilityThree();
        _character.AbilityThreeInput = false;
    }

    public override void OnStateExit()
    {
        _polo.CancelAbilityThree();
        _character.AbilityThreeInput = false;
    }
}
