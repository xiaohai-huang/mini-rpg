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

        private XiaoQiao _character;

        public override void Awake(StateMachine stateMachine)
        {
            _character = stateMachine.GetComponent<XiaoQiao>();
        }

        public override void OnUpdate() { }

        public override async void OnStateEnter()
        {
            _character.SetAbilityInput(Character.Ability.One, false);
            _character.PerformingAbilityOne = true;
            await _character.PerformAbilityOne();
            _character.PerformingAbilityOne = false;
        }

        public override void OnStateExit() { }
    }
}
