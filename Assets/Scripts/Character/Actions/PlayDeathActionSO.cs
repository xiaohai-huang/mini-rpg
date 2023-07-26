using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "PlayDeathAction", menuName = "State Machines/Actions/Play Death Action")]
public class PlayDeathActionSO : StateActionSO
{
    protected override StateAction CreateAction() => new PlayDeathAction();
}

public class PlayDeathAction : StateAction
{
    protected new PlayDeathActionSO OriginSO => (PlayDeathActionSO)base.OriginSO;
    private Animator _animator;
    private static readonly int DEAD_ANIMATION_ID = Animator.StringToHash("Dead");
    public override void Awake(StateMachine stateMachine)
    {
        _animator = stateMachine.GetComponent<Animator>();
    }

    public override void OnUpdate()
    {
    }

    public override void OnStateEnter()
    {
        _animator.SetBool(DEAD_ANIMATION_ID, true);
    }

    public override void OnStateExit()
    {
        _animator.SetBool(DEAD_ANIMATION_ID, false);
    }
}
