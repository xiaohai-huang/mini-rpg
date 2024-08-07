using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

namespace Xiaohai.Character.MarcoPolo
{
    [CreateAssetMenu(
        fileName = "AbilityOneAction",
        menuName = "State Machines/Actions/Marco Polo/Ability One Action"
    )]
    public class AbilityOneActionSO : StateActionSO
    {
        protected override StateAction CreateAction() => new AbilityOneAction();
    }

    public class AbilityOneAction : StateAction
    {
        protected new AbilityOneActionSO OriginSO => (AbilityOneActionSO)base.OriginSO;
        private Character _character;
        private MarcoPolo _polo;

        public override void Awake(StateMachine stateMachine)
        {
            _character = stateMachine.GetComponent<Character>();
            _polo = stateMachine.GetComponent<MarcoPolo>();
        }

        public override void OnUpdate() { }

        public override void OnStateEnter()
        {
            _polo.PerformAbilityOne();
            _character.AbilityOneInput = false;
        }

        public override void OnStateExit() { }
    }
}
