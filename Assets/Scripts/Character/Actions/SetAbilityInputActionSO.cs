using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

namespace Xiaohai.Character.Actions
{
    [CreateAssetMenu(
        fileName = "Set Ability Input Action",
        menuName = "State Machines/Actions/Set Ability Input Action"
    )]
    public class SetAbilityInputActionSO : StateActionSO
    {
        public Character.Ability Ability;
        public bool Value;

        protected override StateAction CreateAction() => new SetAbilityInputAction();
    }

    public class SetAbilityInputAction : StateAction
    {
        private Character _character;
        protected new SetAbilityInputActionSO OriginSO => (SetAbilityInputActionSO)base.OriginSO;

        public override void Awake(StateMachine stateMachine)
        {
            _character = stateMachine.GetComponent<Character>();
        }

        public override void OnUpdate()
        {
            _character.SetAbilityInput(OriginSO.Ability, OriginSO.Value);
        }

        public override void OnStateEnter() { }

        public override void OnStateExit() { }
    }
}
