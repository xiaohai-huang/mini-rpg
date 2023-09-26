using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using Xiaohai.Utilities;

namespace Xiaohai.Turret.Conditions
{
	[CreateAssetMenu(fileName = "EnemyWithinRangeCondition", menuName = "State Machines/Conditions/Turret/Enemy Within Range Condition")]
	public class EnemyWithinRangeConditionSO : StateConditionSO
	{
		protected override Condition CreateCondition() => new EnemyWithinRangeCondition();
	}

	public class EnemyWithinRangeCondition : Condition
	{
		private Turret _turret;

		protected new EnemyWithinRangeConditionSO OriginSO => (EnemyWithinRangeConditionSO)base.OriginSO;
		public override void Awake(StateMachine stateMachine)
		{
			_turret = stateMachine.GetComponent<Turret>();
		}

		protected override bool Statement()
		{
			return _turret.Target != null;
		}
	}
}
