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
		private Collider _turretCollider;
		private TargetPicker _targetPicker;
		public override void Awake(StateMachine stateMachine)
		{
			_turret = stateMachine.GetComponent<Turret>();
			_turretCollider = stateMachine.GetComponent<Collider>();
			_targetPicker = stateMachine.GetComponent<TargetPicker>();
		}

		public override void OnUpdate()
		{
			GameObject target = _targetPicker.Target;
			if (target != null)
			{
				_turret.AttackBallTarget.position = target.transform.position;
			}
		}
		private int _attackTimer;
		public override void OnStateEnter()
		{
			_attackTimer = Timer.Instance.SetInterval(() =>
			{
				GameObject target = _targetPicker.Target;
				if (target == null) return;
				var bullet = Object.Instantiate(_turret.BulletPrefab);
				Physics.IgnoreCollision(bullet.GetComponent<BoxCollider>(), _turretCollider, true);
				var goForward = bullet.GetComponent<GoForward>();
				goForward.transform.position = _turret.FirePoint.position;
				goForward.transform.LookAt(target.transform.position);
				goForward.Speed = 10f;
				goForward.DamageAmount = 250;
			}, 1000f / _turret.AttackSpeed);
		}

		public override void OnStateExit()
		{
			Timer.Instance.ClearInterval(_attackTimer);
		}
	}
}
