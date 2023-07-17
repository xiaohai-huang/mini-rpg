using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

namespace Xiaohai.Character.MarcoPolo
{
    [CreateAssetMenu(fileName = "AbilityOneAction", menuName = "State Machines/Actions/Marco Polo/Ability One Action")]
    public class AbilityOneActionSO : StateActionSO
    {
        protected override StateAction CreateAction() => new AbilityOneAction();
    }

    public class AbilityOneAction : StateAction
    {
        private MarcoPolo _polo;
        protected new AbilityOneActionSO OriginSO => (AbilityOneActionSO)base.OriginSO;

        public override void Awake(StateMachine stateMachine)
        {
            _polo = stateMachine.GetComponent<MarcoPolo>();
        }

        public override void OnUpdate()
        {

        }

        public override void OnStateEnter()
        {
            _polo.ShouldPerformingAbilityOne = true;
        }

        public override void OnStateExit()
        {
        }
    }
}
