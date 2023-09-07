using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

using Xiaohai.Utilities;

namespace Xiaohai.Character.Arthur
{
	[CreateAssetMenu(fileName = "AbilityThreeAction", menuName = "State Machines/Actions/Arthur/Ability Three Action")]
	public class AbilityThreeActionSO : StateActionSO
	{
		public float AttackRange = 10f;
		protected override StateAction CreateAction() => new AbilityThreeAction();
	}

	/// <summary>
	/// Arthur raises his sword and leaps towards an enemy hero, dealing 16/20/24% magic damage to the enemy hero's maximum health and knocking them up for 0.5 seconds.
	/// At the same time, he summons a holy seal to cover the landing area.
	/// The holy seal deals 85/105/125 points of magic damage per second to enemies within range for 5 seconds.
	/// </summary>
	public class AbilityThreeAction : StateAction
	{
		protected new AbilityThreeActionSO OriginSO => (AbilityThreeActionSO)base.OriginSO;
		private Arthur _character;
		private NearByUnits _nearByUnits;
		private Animator _animator;
		public override void Awake(StateMachine stateMachine)
		{
			_character = stateMachine.GetComponent<Arthur>();
			_nearByUnits = stateMachine.GetComponent<NearByUnits>();
			_animator = stateMachine.GetComponent<Animator>();
		}

		public override void OnUpdate()
		{
		}

		public override void OnStateEnter()
		{
			// consume the input
			_character.AbilityThreeInput = false;
			_character.PerformingAbilityThree = true;
			var target = _nearByUnits.GetClosest();
			if (target != null)
			{
				if (Vector3.Distance(target.transform.position, _character.transform.position) <= OriginSO.AttackRange)
				{
					var targetDamageable = target.GetComponent<Damageable>();
					var targetHealth = target.GetComponent<Health>();

					// leap towards the enemy
					_character.LeapTowardsEnemy(target, () =>
					{
						// dealing 16/20/24% magic damage to the enemy hero's maximum health
						float damageAmount = 0.16f * targetHealth.MaxHealth;
						targetDamageable.TakeDamage((int)damageAmount);

						_character.PerformingAbilityThree = false;
					});

				}
				else
				{
					_character.PerformingAbilityThree = false;
				}
			}
			else
			{
				_character.PerformingAbilityThree = false;
			}
		}

		public override void OnStateExit()
		{
		}
	}
}
