using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

namespace Xiaohai.Character.Arthur
{
    [CreateAssetMenu(fileName = "Move Towards Target", menuName = "State Machines/Actions/Move Towards Target Action")]
    public class MoveTowardsTargetActionSO : StateActionSO
    {
        public RuntimeTransformAnchor Target;
        protected override StateAction CreateAction() => new MoveTowardsTargetAction();
    }

    public class MoveTowardsTargetAction : StateAction
    {
        private NavMeshAgent _navMeshAgent;
        private CharacterController _characterController;
        CancellationTokenSource _cts;

        protected new MoveTowardsTargetActionSO OriginSO => (MoveTowardsTargetActionSO)base.OriginSO;
        public override void Awake(StateMachine stateMachine)
        {
            _navMeshAgent = stateMachine.GetComponent<NavMeshAgent>();
            _characterController = stateMachine.GetComponent<CharacterController>();
        }
        public override void OnUpdate()
        {

        }

        public override void OnStateEnter()
        {
            _characterController.enabled = false;
            _navMeshAgent.enabled = true;
            _cts = new CancellationTokenSource();
            MoveTowards(OriginSO.Target.Value);
        }

        public override void OnStateExit()
        {
            _characterController.enabled = true;
            _navMeshAgent.isStopped = true;
            _navMeshAgent.enabled = false;
            _cts.Cancel();
            _cts.Dispose();
        }

        private async void MoveTowards(Transform targetTransform)
        {
            while (!_cts.IsCancellationRequested)
            {
                _navMeshAgent.SetDestination(targetTransform.position);

                if (!_navMeshAgent.pathPending)
                {
                    if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
                    {
                        if (!_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude == 0f)
                        {
                            break;
                        }
                    }
                }
                await Task.Yield();
            }
        }
    }
}