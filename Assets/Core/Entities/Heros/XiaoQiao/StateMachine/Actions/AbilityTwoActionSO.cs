using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

namespace Xiaohai.Character.XiaoQiao
{
    [CreateAssetMenu(
        fileName = "AbilityTwoAction",
        menuName = "State Machines/Actions/Xiao Qiao/Ability Two Action"
    )]
    public class AbilityTwoActionSO : StateActionSO
    {
        protected override StateAction CreateAction() => new AbilityTwoAction();
    }

    public class AbilityTwoAction : StateAction
    {
        protected new AbilityTwoActionSO OriginSO => (AbilityTwoActionSO)base.OriginSO;

        private XiaoQiao _character;

        public override void Awake(StateMachine stateMachine)
        {
            _character = stateMachine.GetComponent<XiaoQiao>();
        }

        public override void OnUpdate() { }

        public override async void OnStateEnter()
        {
            _character.SetAbilityInput(Character.Ability.Two, false);
            _character.PerformingAbilityTwo = true;
            await _character.PerformAbilityTwo();
            _character.PerformingAbilityTwo = false;
        }

        public override void OnStateExit() { }
    }
}
