using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

namespace Xiaohai.Character.Conditions
{
	public enum Operator
	{
		Equal,
		Greater,
		Less
	}
	[CreateAssetMenu(fileName = "DistanceCondition", menuName = "State Machines/Conditions/Distance Condition")]
	public class DistanceConditionSO : StateConditionSO
	{
		public RuntimeTransformAnchor TargetTransform;
		public float Distance;
		public Operator Operator;
		protected override Condition CreateCondition() => new DistanceCondition();
	}

	public class DistanceCondition : Condition
	{
		private Transform _transform;
		protected new DistanceConditionSO OriginSO => (DistanceConditionSO)base.OriginSO;

		public override void Awake(StateMachine stateMachine)
		{
			_transform = stateMachine.transform;
		}

		protected override bool Statement()
		{
			if (OriginSO.TargetTransform.Value == null) return true;

			float dist = Vector3.Distance(OriginSO.TargetTransform.Value.position, _transform.position);
			// Debug.Log($"dist between {_transform} & {OriginSO.TargetTransform.Value} is {dist}");
			switch (OriginSO.Operator)
			{
				case Operator.Equal:
					{
						return dist == OriginSO.Distance;
					}
				case Operator.Greater:
					{
						return dist > OriginSO.Distance;
					}
				case Operator.Less:
					{
						return dist < OriginSO.Distance;
					}
				default:
					{
						return false;
					}
			}
		}

		public override void OnStateEnter()
		{
		}

		public override void OnStateExit()
		{
		}
	}
}
