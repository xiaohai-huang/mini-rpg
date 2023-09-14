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
        private Character _character;
        CancellationTokenSource _cts;

        protected new MoveTowardsTargetActionSO OriginSO => (MoveTowardsTargetActionSO)base.OriginSO;
        public override void Awake(StateMachine stateMachine)
        {
            _navMeshAgent = stateMachine.GetComponent<NavMeshAgent>();
            _character = stateMachine.GetComponent<Character>();
        }
        public override void OnUpdate()
        {

        }

        public override void OnStateEnter()
        {
            _character.IsAutoMove = true;
            _navMeshAgent.enabled = true;
            _navMeshAgent.updatePosition = false;
            _navMeshAgent.updateRotation = false;
            _cts = new CancellationTokenSource();
            MoveTowards(OriginSO.Target.Value);
        }

        public override void OnStateExit()
        {
            _character.IsAutoMove = false;
            _navMeshAgent.isStopped = true;
            _navMeshAgent.enabled = false;
            _cts.Cancel();
            _cts.Dispose();
        }

        private async void MoveTowards(Transform targetTransform)
        {
            while (!_cts.IsCancellationRequested && !Application.exitCancellationToken.IsCancellationRequested)
            {
                _navMeshAgent.SetDestination(targetTransform.position);
                var t = _navMeshAgent.steeringTarget;
                Vector3 direction = (t - _character.transform.position).normalized;
                _character.HorizontalAutoInput = new Vector2(direction.x, direction.z);

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
                _character.Velocity = _navMeshAgent.velocity;
                await Task.Yield();
            }
        }
    }
}