using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

namespace Xiaohai.Character.MarcoPolo
{
    [CreateAssetMenu(fileName = "Basic Attack Action", menuName = "State Machines/Actions/Marco Polo/Basic Attack Action")]
    public class BasicAttackActionSO : StateActionSO
    {
        protected override StateAction CreateAction() => new FireBulletAction();
    }

    public class FireBulletAction : StateAction
    {
        protected new BasicAttackActionSO OriginSO => (BasicAttackActionSO)base.OriginSO;
        private Character _character;
        private MarcoPolo _polo;

        public override void Awake(StateMachine stateMachine)
        {
            _character = stateMachine.GetComponent<Character>();
            _polo = stateMachine.GetComponent<MarcoPolo>();
        }

        public override void OnUpdate()
        {
        }

        public override void OnStateEnter()
        {
            _polo.Attack();
            _character.BasicAttackInput = false;
        }

        public override void OnStateExit()
        {
        }
    }
}
