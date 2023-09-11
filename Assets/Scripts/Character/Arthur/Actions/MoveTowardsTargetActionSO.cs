using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

namespace Xiaohai.Character.Arthur
{
    [CreateAssetMenu(fileName = "Move Towards Target", menuName = "State Machines/Actions/Arthur/Move Towards Target Action")]
    public class MoveTowardsTargetActionSO : StateActionSO
    {
        protected override StateAction CreateAction() => new MoveTowardsTargetAction();
    }

    public class MoveTowardsTargetAction : StateAction
    {
        public override void OnUpdate()
        {

        }
    }
}