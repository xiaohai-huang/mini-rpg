using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

namespace Xiaohai.Character.Arthur
{
	[CreateAssetMenu(fileName = "AbilityTwoAction", menuName = "State Machines/Actions/Arthur/Ability Two Action")]
	public class AbilityTwoActionSO : StateActionSO
	{
		/// <summary>
		/// The amount of time that shields last.
		/// </summary>
		public float SHIELDS_DURATION = 5000.0f;
		public float SHIELD_PHYSICAL_DAMAGE_AMOUNT = 145.0f;
		public RuntimeTransformAnchor Shields;
		public AbilityTwoEffectSO AbilityTwoEffectSO;
		protected override StateAction CreateAction() => new AbilityTwoAction();
	}

	/// <summary>
	/// Arthur summons 3 shields that circle around him, 
	/// enemies that are struck by the shield will receive 145/163/181/199/217/235 (+80% physical bonus) Physical Damage. 
	/// Shields last for 5 seconds.
	/// </summary>
	public class AbilityTwoAction : StateAction
	{
		protected new AbilityTwoActionSO OriginSO => (AbilityTwoActionSO)base.OriginSO;
		private Character _character;
		private EffectSystem _effectSystem;
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
			_character.AbilityTwoInput = false;
			AbilityTwoEffect effect = OriginSO.AbilityTwoEffectSO.CreateEffect();
			effect.Init(OriginSO.SHIELD_PHYSICAL_DAMAGE_AMOUNT, OriginSO.SHIELDS_DURATION, OriginSO.Shields.Value.GetComponent<Shields>());
			_effectSystem.AddEffect(effect);
		}

		public override void OnStateExit()
		{
		}
	}
}