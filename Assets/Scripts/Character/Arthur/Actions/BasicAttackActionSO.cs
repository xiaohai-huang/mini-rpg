using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

namespace Xiaohai.Character.Arthur
{
	[CreateAssetMenu(fileName = "BasicAttackAction", menuName = "State Machines/Actions/Arthur/Basic Attack Action")]
	public class BasicAttackActionSO : StateActionSO
	{
		protected override StateAction CreateAction() => new BasicAttackAction();
	}

	public class BasicAttackAction : StateAction
	{
		protected new BasicAttackActionSO OriginSO => (BasicAttackActionSO)base.OriginSO;
		private Animator _animator;
		private Character _character;
		private static readonly int BASIC_ATTACK_ANIMATION_ID = Animator.StringToHash("Basic Attack");
		public override void Awake(StateMachine stateMachine)
		{
			_animator = stateMachine.GetComponent<Animator>();
			_character = stateMachine.GetComponent<Character>();
		}

		public override void OnUpdate()
		{
		}
		private int _timer;
		public override void OnStateEnter()
		{
			_character.PerformingBasicAttack = true;
			_character.BasicAttackInput = false;
			_animator.SetTrigger(BASIC_ATTACK_ANIMATION_ID);
			_timer = Timer.Instance.SetTimeout(() => { _character.PerformingBasicAttack = false; }, 500.0f);
		}

		public override void OnStateExit()
		{
			Timer.Instance.ClearTimeout(_timer);
		}
	}
}
