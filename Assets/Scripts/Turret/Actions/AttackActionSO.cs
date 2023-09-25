using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using Xiaohai.Character;

namespace Xiaohai.Turret.Actions
{
	[CreateAssetMenu(fileName = "AttackAction", menuName = "State Machines/Actions/Turret/Attack Action")]
	public class AttackActionSO : StateActionSO
	{
		protected override StateAction CreateAction() => new AttackAction();
	}

	public class AttackAction : StateAction
	{
		protected new AttackActionSO OriginSO => (AttackActionSO)base.OriginSO;
		private Turret _turret;
		private TargetPicker _targetPicker;
		private GameObject _target;

		public override void Awake(StateMachine stateMachine)
		{
			_turret = stateMachine.GetComponent<Turret>();
			_targetPicker = stateMachine.GetComponent<TargetPicker>();
		}

		public override void OnUpdate()
		{
			if (_target != null)
			{
				_turret.AttackBallTarget.position = _target.transform.position;
			}
		}
		private int _attackTimer;
		private int _startAttackTimer;
		public override void OnStateEnter()
		{
			_target = _targetPicker.Target;
			// Delay for 500ms and then start the attack
			_startAttackTimer = Timer.Instance.SetTimeout(() =>
			{
				_attackTimer = Timer.Instance.SetInterval(() =>
							{
								if (_target == null) return;
								// Pick a new target if the current one does not stay within the attack range
								if (Vector3.Distance(_turret.transform.position, _target.transform.position) > _turret.AttackRange)
								{
									_target = _targetPicker.Target;
									if (_target == null) return;
								}
								var projectile = Object.Instantiate(_turret.BulletPrefab);
								projectile.SetTarget(_target.GetComponent<Damageable>());
								projectile.transform.position = _turret.FirePoint.position;
								projectile.Speed = 10f;
								projectile.DamageAmount = 250;
							}, 1000f / _turret.AttackSpeed, true);
			}, 500f);
		}

		public override void OnStateExit()
		{
			Timer.Instance.ClearTimeout(_startAttackTimer);
			Timer.Instance.ClearInterval(_attackTimer);
			_target = null;
		}
	}
}
