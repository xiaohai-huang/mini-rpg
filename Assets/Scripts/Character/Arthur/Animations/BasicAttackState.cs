using UnityEngine;

namespace Xiaohai.Character.Arthur
{
    public class BasicAttackState : StateMachineBehaviour
    {
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        // override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        // {
        // }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var character = animator.GetComponent<Character>();
            character.PerformingBasicAttack = false;
        }
    }
}
