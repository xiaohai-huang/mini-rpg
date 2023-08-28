using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

namespace Xiaohai.Character.Arthur
{
	[CreateAssetMenu(fileName = "AbilityOneAction", menuName = "State Machines/Actions/Arthur/Ability One Action")]
	public class AbilityOneActionSO : StateActionSO
	{
		public AbilityOneEffectSO AbilityOneEffectSO;
		protected override StateAction CreateAction() => new AbilityOneAction();
	}

	/// <summary>
	/// 亚瑟增加30%移动速度，持续3秒，下一次普通攻击变更为跳斩，跳斩会造成180/205/230/255/280/305（+100%物理加成）点物理伤害并将目标沉默1.25秒，
	/// 同时跳斩命中的敌方英雄会被标记，持续5秒，亚瑟的普通攻击和技能会额外造成目标最大生命值1%的法术伤害.
	/// </summary>
	public class AbilityOneAction : StateAction
	{
		protected new AbilityOneActionSO OriginSO => (AbilityOneActionSO)base.OriginSO;
		private EffectSystem _effectSystem;
		private Character _character;
		public override void Awake(StateMachine stateMachine)
		{
			_character = stateMachine.GetComponent<Character>();
			_effectSystem = stateMachine.GetComponent<EffectSystem>();
		}

		public override void OnUpdate()
		{
		}

		public override void OnStateEnter()
		{
			var effect = (AbilityOneEffect)OriginSO.AbilityOneEffectSO.CreateEffect();
			effect.Init(0.3f, 3000f);
			_effectSystem.AddEffect(effect);
			_character.AbilityOneInput = false;
		}

		public override void OnStateExit()
		{
		}
	}
}
