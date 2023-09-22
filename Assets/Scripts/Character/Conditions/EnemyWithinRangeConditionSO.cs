using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

namespace Xiaohai.Character.Conditions
{
	[CreateAssetMenu(fileName = "EnemyWithinRangeCondition", menuName = "State Machines/Conditions/Enemy Within Range Condition")]
	public class EnemyWithinRangeConditionSO : StateConditionSO
	{
		protected override Condition CreateCondition() => new EnemyWithinRangeCondition();
	}

	public class EnemyWithinRangeCondition : Condition
	{
		protected new EnemyWithinRangeConditionSO OriginSO => (EnemyWithinRangeConditionSO)base.OriginSO;
		private TargetPicker _targetPicker;
		public override void Awake(StateMachine stateMachine)
		{
			_targetPicker = stateMachine.GetComponent<TargetPicker>();
		}

		protected override bool Statement()
		{
			if (_targetPicker == null)
			{
				throw new System.Exception("This game object does not have TargetPicker component.");
			}
			return _targetPicker.Target != null;
		}
	}
}
