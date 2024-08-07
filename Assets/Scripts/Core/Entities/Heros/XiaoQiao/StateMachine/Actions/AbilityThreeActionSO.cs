using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

namespace Xiaohai.Character.XiaoQiao
{
    [CreateAssetMenu(
        fileName = "AbilityThreeAction",
        menuName = "State Machines/Actions/Xiao Qiao/Ability Three Action"
    )]
    public class AbilityThreeActionSO : StateActionSO
    {
        protected override StateAction CreateAction() => new AbilityThreeAction();
    }

    public class AbilityThreeAction : StateAction
    {
        protected new AbilityThreeActionSO OriginSO => (AbilityThreeActionSO)base.OriginSO;

        private XiaoQiao _character;

        public override void Awake(StateMachine stateMachine)
        {
            _character = stateMachine.GetComponent<XiaoQiao>();
        }

        public override void OnUpdate() { }

        public override void OnStateEnter()
        {
            _character.SetAbilityInput(Character.Ability.Three, false);
            _character.PerformingAbilityThree = true;
            _character.PerformAbilityThree();
            _character.PerformingAbilityThree = false;
        }

        public override void OnStateExit() { }
    }
}
