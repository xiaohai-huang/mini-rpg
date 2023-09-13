using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "SetBoolAnimatorParameterActionSO", menuName = "State Machines/Actions/Set Bool Animator Param Action")]
public class SetBoolAnimatorParameterActionSO : StateActionSO
{
	public string Parameter;
	public bool Value;
	protected override StateAction CreateAction() => new SetBoolAnimatorParamAction();
}

public class SetBoolAnimatorParamAction : StateAction
{
	protected new SetBoolAnimatorParameterActionSO OriginSO => (SetBoolAnimatorParameterActionSO)base.OriginSO;

	private Animator _animator;
	private int ANIMATION_ID;
	public override void Awake(StateMachine stateMachine)
	{
		_animator = stateMachine.GetComponent<Animator>();
		ANIMATION_ID = Animator.StringToHash(OriginSO.Parameter);
	}

	public override void OnUpdate()
	{
	}

	public override void OnStateEnter()
	{
		_animator.SetBool(ANIMATION_ID, OriginSO.Value);
	}

	public override void OnStateExit()
	{
	}
}
