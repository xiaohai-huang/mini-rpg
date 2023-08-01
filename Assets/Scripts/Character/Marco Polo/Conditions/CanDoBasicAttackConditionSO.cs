using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

namespace Xiaohai.Character.MarcoPolo
{
    [CreateAssetMenu(fileName = "Can Do Basic Attack Condition", menuName = "State Machines/Conditions/Marco Polo/Can Do Basic Attack Condition")]
    public class CanDoBasicAttackConditionSO : StateConditionSO
    {
        protected override Condition CreateCondition() => new CanDoBasicAttackCondition();
    }

    public class CanDoBasicAttackCondition : Condition
    {
        protected new CanDoBasicAttackConditionSO OriginSO => (CanDoBasicAttackConditionSO)base.OriginSO;
        private MarcoPolo _polo;
        public override void Awake(StateMachine stateMachine)
        {
            _polo = stateMachine.GetComponent<MarcoPolo>();
        }

        protected override bool Statement()
        {
            return _polo.CanDoBasicAttack;
        }

        public override void OnStateEnter()
        {
        }

        public override void OnStateExit()
        {
        }
    }
}
