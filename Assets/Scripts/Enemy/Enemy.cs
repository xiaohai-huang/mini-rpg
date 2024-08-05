using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityHFSM;
using Xiaohai.Character;
using Xiaohai.Utilities;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Enemy : MonoBehaviour
{
    private StateMachine _fsm;

    [SerializeField]
    private bool _static = false;

    [ReadOnly]
    public string CurrentState;

    [SerializeField]
    private Damageable _target;
    public float DetectRange = 10f;
    public float AttackRange = 1.5f;
    public float AttackDegrees = 30f;
    private NavMeshAgent _agent;
    private Damageable _damageable;
    private Rigidbody _rigidbody;
    private AttackHandler _attackHandler;
    private Coroutine _updateNearbyTargetCoroutine;

    /// <summary>
    /// Crowd Control duration
    /// </summary>
    private float _ccDuration;
    private Canvas ui;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _damageable = GetComponent<Damageable>();
        _attackHandler = GetComponent<AttackHandler>();
        _rigidbody = GetComponent<Rigidbody>();
        ui = GetComponentInChildren<Canvas>();
    }

    void Start()
    {
        _updateNearbyTargetCoroutine = StartCoroutine(UpdateNearbyTarget());
        _fsm = new StateMachine(this);
        _fsm.AddState("Idle");

        _fsm.AddState(
            "CrowdControl",
            onEnter: (s) =>
            {
                _agent.enabled = false;
            },
            onExit: (s) => _agent.enabled = true
        );
        if (!_static)
        {
            _fsm.AddState(
                "FollowPlayer",
                onEnter: (s) =>
                {
                    _agent.isStopped = false;
                },
                onLogic: (state) =>
                {
                    MoveTowardsPlayer();
                },
                onExit: (s) =>
                {
                    _agent.isStopped = true;
                }
            );
            _fsm.AddState(
                "Attacking",
                onLogic: (state) =>
                {
                    AttackPlayer();
                }
            );
            _fsm.AddState(
                "Defeat",
                onEnter: (state) =>
                {
                    // hide UI
                    ui.gameObject.SetActive(false);

                    // shrink the enemy and turn red
                    transform.localScale = Vector3.one * 0.5f;
                    var mat = GetComponentInChildren<MeshRenderer>().material;
                    mat.color = Color.red;

                    var collider = GetComponent<CapsuleCollider>();
                    collider.enabled = false;

                    _agent.enabled = false;
                    StopCoroutine(_updateNearbyTargetCoroutine);
                    Destroy(gameObject, 10);
                }
            );

            _fsm.AddTransitionFromAny(
                new Transition("", "Defeat", (transition) => _damageable.IsDead)
            );
            _fsm.AddTransition(
                "Idle",
                "FollowPlayer",
                (transition) =>
                {
                    return IsPlayerAlive()
                        && DistanceToPlayer() <= DetectRange
                        && !IsPlayerVisible();
                }
            );
            _fsm.AddTransition(
                "FollowPlayer",
                "Idle",
                (transition) =>
                {
                    return DistanceToPlayer() > DetectRange;
                }
            );
            _fsm.AddTransition(
                "Idle",
                "Attacking",
                (transition) =>
                    IsPlayerAlive() && DistanceToPlayer() <= AttackRange && IsPlayerVisible()
            );
            _fsm.AddTransition(
                "FollowPlayer",
                "Attacking",
                (transition) =>
                    IsPlayerAlive() && DistanceToPlayer() <= AttackRange && IsPlayerVisible()
            );
            _fsm.AddTransition(
                "Attacking",
                "FollowPlayer",
                (transition) => DistanceToPlayer() > AttackRange || !IsPlayerVisible()
            );
            _fsm.AddTransition("CrowdControl", "Idle", t => _ccDuration <= 0);
            _fsm.AddTransitionFromAny("Idle", t => !_damageable.IsDead && !IsPlayerAlive());
        }

        _fsm.SetStartState("Idle");
        _fsm.Init();
    }

    // bug: if there are multiple colliders, it will not be able to find the player
    private readonly Collider[] _colliders = new Collider[32];

    IEnumerator UpdateNearbyTarget()
    {
        while (true)
        {
            int numColliders = Physics.OverlapSphereNonAlloc(
                transform.position,
                DetectRange,
                _colliders
            );
            bool hasTarget = false;
            for (int i = 0; i < numColliders; i++)
            {
                if (_colliders[i].CompareTag("Player"))
                {
                    _target = _colliders[i].GetComponent<Damageable>();
                    hasTarget = true;
                    break;
                }
            }
            if (!hasTarget)
                _target = null;
            yield return new WaitForSeconds(0.5f);
        }
    }

    float DistanceToPlayer()
    {
        if (_target == null)
            return float.MaxValue;
        var playerPosition = _target.transform.position;
        float distance = Vector3.Distance(transform.position, playerPosition);
        return distance;
    }

    /// <summary>
    /// Check if the player is blocked by obstacles
    /// </summary>
    /// <returns>false if it is blocked by other obstacles</returns>
    bool IsPlayerVisible()
    {
        if (
            Physics.CapsuleCast(
                transform.position,
                transform.position + Vector3.up,
                _agent.radius,
                transform.forward,
                out RaycastHit hit
            )
        )
        {
            if (hit.collider.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }

    bool IsPlayerAlive()
    {
        if (_target == null)
            return false;
        bool alive = !_target.IsDead;
        return alive;
    }

    void MoveTowardsPlayer()
    {
        _agent.SetDestination(_target.transform.position);

        if (_agent.remainingDistance < _agent.stoppingDistance)
        {
            transform.LookAt(_target.transform);
        }
    }

    float _timer;
    public float AttackInterval = 2f;

    void AttackPlayer()
    {
        var playerPosition = _target.transform.position;
        var direction = playerPosition - transform.position;
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.LookRotation(direction.normalized, Vector3.up),
            4 * Time.deltaTime
        );
        if (_timer <= 0)
        {
            var degrees = Vector3.Angle(transform.forward, direction);
            if (degrees <= AttackDegrees)
            {
                _attackHandler.Attack();
                _timer = AttackInterval;
            }
        }
        else
        {
            _timer -= Time.deltaTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        _fsm.OnLogic();
        CurrentState = _fsm.ActiveStateName;
        if (_ccDuration > 0)
        {
            _ccDuration -= Time.deltaTime;
        }
    }

    void Launch(float duration, float angle = 90f)
    {
        float initialVelocity = (-Physics.gravity.y) * duration / 2;
        float v0x = initialVelocity * Mathf.Cos(angle * Mathf.Deg2Rad);
        float v0y = initialVelocity * Mathf.Sin(angle * Mathf.Deg2Rad);
        _rigidbody.linearVelocity = new Vector3(v0x, v0y, 0);
    }

    public void KnockUp(float duration)
    {
        if (_fsm.ActiveStateName == "Defeat")
            return;
        Launch(duration);

        float diff = duration - _ccDuration;
        _ccDuration += diff;
        _fsm.RequestStateChange("CrowdControl");
    }

#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        Handles.color = Color.yellow;
        Handles.DrawWireDisc(transform.position, Vector3.up, DetectRange);
    }
#endif
}
