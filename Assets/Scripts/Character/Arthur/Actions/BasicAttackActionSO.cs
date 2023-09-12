using System.Threading.Tasks;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

namespace Xiaohai.Character.Arthur
{
	[CreateAssetMenu(fileName = "BasicAttackAction", menuName = "State Machines/Actions/Arthur/Basic Attack Action")]
	public class BasicAttackActionSO : StateActionSO
	{
		/// <summary>
		/// The amount of time in ms it takes to finish the attack.
		/// </summary> 
		public float AttackTime = 500.0f;
		public float AttackRange = 1.25f;
		protected override StateAction CreateAction() => new BasicAttackAction();
	}

	public class BasicAttackAction : StateAction
	{
		protected new BasicAttackActionSO OriginSO => (BasicAttackActionSO)base.OriginSO;
		private Animator _animator;
		private Character _character;
		private TargetPicker _targetPicker;

		private static readonly int BASIC_ATTACK_ANIMATION_ID = Animator.StringToHash("Basic Attack");
		public override void Awake(StateMachine stateMachine)
		{
			_animator = stateMachine.GetComponent<Animator>();
			_character = stateMachine.GetComponent<Character>();
			_targetPicker = stateMachine.GetComponent<TargetPicker>();
		}

		public override void OnUpdate()
		{
		}
		private int _timer;
		public override void OnStateEnter()
		{
			_character.BasicAttackInput = false;
			DoBasicAttack();
		}

		public override void OnStateExit()
		{
			Timer.Instance.ClearTimeout(_timer);
		}

		private async void DoBasicAttack()
		{
			_character.PerformingBasicAttack = true;
			var target = _targetPicker.Target;
			if (target != null)
			{
				Vector3 direction = (target.transform.position - _character.transform.position).normalized;
				// face towards the target
				while (!(Vector3.Dot(_character.transform.forward, direction) > 0.99f))
				{
					var targetRotation = Quaternion.LookRotation(direction, Vector3.up);
					_character.transform.rotation = Quaternion.Slerp(_character.transform.rotation, targetRotation, 5f * Time.deltaTime);

					await Task.Yield();
				}
			}

			_animator.SetTrigger(BASIC_ATTACK_ANIMATION_ID);
			_timer = Timer.Instance.SetTimeout(() => { _character.PerformingBasicAttack = false; }, OriginSO.AttackTime);
		}
	}
}
