using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

namespace Xiaohai.Character.Arthur
{
	[CreateAssetMenu(fileName = "AbilityOneAction", menuName = "State Machines/Actions/Arthur/Ability One Action")]
	public class AbilityOneActionSO : StateActionSO
	{
		public float SpeedUpPercentage;
		/// <summary>
		/// The speed up duration in ms.
		/// </summary>
		public float SpeedUpDuration;
		public float EnhancedBasicAttackRange = 5f;
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
		private Animator _animator;
		private static readonly int ABILITY_ONE_PREPARE_ANIMATION_ID = Animator.StringToHash("Ability One Preparation");
		private static readonly int ABILITY_ONE_SPEED_UP_ANIMATION_ID = Animator.StringToHash("Ability One Speed Up");
		public override void Awake(StateMachine stateMachine)
		{
			_character = stateMachine.GetComponent<Character>();
			_effectSystem = stateMachine.GetComponent<EffectSystem>();
			_animator = stateMachine.GetComponent<Animator>();
		}

		public override void OnUpdate()
		{
		}

		public override void OnStateEnter()
		{
			var effect = OriginSO.AbilityOneEffectSO.CreateEffect();
			effect.Init(OriginSO.SpeedUpPercentage, OriginSO.SpeedUpDuration, OriginSO.EnhancedBasicAttackRange);
			_effectSystem.AddEffect(effect);
			_animator.SetTrigger(ABILITY_ONE_PREPARE_ANIMATION_ID);
			_animator.SetBool(ABILITY_ONE_SPEED_UP_ANIMATION_ID, true);
			Timer.Instance.SetTimeout(() =>
			{
				_animator.SetBool(ABILITY_ONE_SPEED_UP_ANIMATION_ID, false);
			}, OriginSO.SpeedUpDuration);
			_character.AbilityOneInput = false;
		}

		public override void OnStateExit()
		{
		}
	}
}
