using Core.Game.Entities.Heros.XiaoQiao;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

namespace Xiaohai.Character.XiaoQiao
{
    [CreateAssetMenu(
        fileName = "AbilityOneAction",
        menuName = "State Machines/Actions/Xiao Qiao/Ability One Action"
    )]
    public class AbilityOneActionSO : StateActionSO
    {
        protected override StateAction CreateAction() => new AbilityOneAction();
    }

    public class AbilityOneAction : StateAction
    {
        protected new AbilityOneActionSO OriginSO => (AbilityOneActionSO)base.OriginSO;

        private Character _character;
        private AbilityOne _abilityOne;

        public override void Awake(StateMachine stateMachine)
        {
            _character = stateMachine.GetComponent<Character>();
            _abilityOne = stateMachine.GetComponent<AbilityOne>();
        }

        public override void OnUpdate() { }

        public override async void OnStateEnter()
        {
            _character.SetAbilityInput(Character.Ability.One, false);
            await _abilityOne.Perform();
        }

        public override void OnStateExit() { }
    }
}
