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
        public float Duration = 0.5f;

        protected override StateAction CreateAction() => new AbilityOneAction();
    }

    public class AbilityOneAction : StateAction
    {
        protected new AbilityOneActionSO OriginSO => (AbilityOneActionSO)base.OriginSO;

        private XiaoQiao _character;
        private int _timer;

        public override void Awake(StateMachine stateMachine)
        {
            _character = stateMachine.GetComponent<XiaoQiao>();
        }

        public override void OnUpdate() { }

        public override void OnStateEnter()
        {
            _character.SetAbilityInput(Character.Ability.One, false);
            _character.PerformingAbilityOne = true;
            _character.PerformAbilityOne();
            _timer = Timer.Instance.SetTimeout(
                () =>
                {
                    _character.PerformingAbilityOne = false;
                },
                OriginSO.Duration * 1000f
            );
        }

        public override void OnStateExit()
        {
            Timer.Instance.ClearTimeout(_timer);
        }
    }
}
