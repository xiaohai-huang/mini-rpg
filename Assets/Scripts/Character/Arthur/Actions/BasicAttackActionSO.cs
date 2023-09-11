using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using Xiaohai.Utilities;

namespace Xiaohai.Character.Arthur
{
	[CreateAssetMenu(fileName = "BasicAttackAction", menuName = "State Machines/Actions/Arthur/Basic Attack Action")]
	public class BasicAttackActionSO : StateActionSO
	{
		/// <summary>
		/// The amount of time in ms it takes to finish the attack.
		/// </summary> 
		public float AttackTime = 500.0f;
		public float AttackRange = 1.5f;
		protected override StateAction CreateAction() => new BasicAttackAction();
	}

	public class BasicAttackAction : StateAction
	{
		protected new BasicAttackActionSO OriginSO => (BasicAttackActionSO)base.OriginSO;
		private Animator _animator;
		private Character _character;
		private NearByUnits _nearByUnits;
		private CharacterController _characterController;
		private NavMeshAgent _navMeshAgent;

		private static readonly int BASIC_ATTACK_ANIMATION_ID = Animator.StringToHash("Basic Attack");
		public override void Awake(StateMachine stateMachine)
		{
			_animator = stateMachine.GetComponent<Animator>();
			_character = stateMachine.GetComponent<Character>();
			_nearByUnits = stateMachine.GetComponent<NearByUnits>();
			_characterController = stateMachine.GetComponent<CharacterController>();
			_navMeshAgent = stateMachine.GetComponent<NavMeshAgent>();
		}

		public override void OnUpdate()
		{
		}
		private int _timer;
		public override void OnStateEnter()
		{
			_character.BasicAttackInput = false;
			// DoBasicAttackWithAbilityOneEffect();
			DoBasicAttack();
		}

		public override void OnStateExit()
		{
			Timer.Instance.ClearTimeout(_timer);
		}

		private const float DESTINATION_OFFSET = 1.25f;
		private async void DoBasicAttackWithAbilityOneEffect()
		{
			_character.PerformingBasicAttack = true;
			var target = _nearByUnits.GetClosest();

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

				if (Vector3.Distance(_character.transform.position, target.transform.position) > DESTINATION_OFFSET)
				{
					// walk towards the target
					Vector3 destination = Navigation.GetLocationWithOffset(target.transform.position, _character.transform.position, DESTINATION_OFFSET);

					_characterController.enabled = false;

#if UNITY_EDITOR
					Debug.DrawLine(_character.transform.position, destination, Color.blue, 5f);
#endif


					// move towards the target
					while (_character.transform.position != destination)
					{
						_character.transform.position = Vector3.MoveTowards(_character.transform.position, destination, Time.deltaTime * 10f);

						await Task.Yield();
					}
					_characterController.enabled = true;
				}

			}

			_animator.SetTrigger(BASIC_ATTACK_ANIMATION_ID);
			_timer = Timer.Instance.SetTimeout(() => { _character.PerformingBasicAttack = false; }, OriginSO.AttackTime);
		}

		private async void DoBasicAttack()
		{
			_character.PerformingBasicAttack = true;
			var target = _nearByUnits.GetClosest();
			_characterController.enabled = false;

			_navMeshAgent.enabled = true;
			_navMeshAgent.SetDestination(target.transform.position);

			// _characterController.enabled = true;
			while (true)
			{
				// Check if we've reached the destination
				if (!_navMeshAgent.pathPending)
				{
					if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
					{
						if (!_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude == 0f)
						{
							// Done
							break;
						}
					}
				}
				await Task.Yield();
			}
			_navMeshAgent.isStopped = true;
			_navMeshAgent.enabled = false;

			Vector3 direction = (target.transform.position - _character.transform.position).normalized;
			// face towards the target
			while (!(Vector3.Dot(_character.transform.forward, direction) > 0.99f))
			{
				var targetRotation = Quaternion.LookRotation(direction, Vector3.up);
				_character.transform.rotation = Quaternion.Slerp(_character.transform.rotation, targetRotation, 5f * Time.deltaTime);

				await Task.Yield();
			}

			_characterController.enabled = true;

			_animator.SetTrigger(BASIC_ATTACK_ANIMATION_ID);
			_timer = Timer.Instance.SetTimeout(() => { _character.PerformingBasicAttack = false; }, OriginSO.AttackTime);
		}
	}
}
