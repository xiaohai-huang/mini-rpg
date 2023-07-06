using FSM;
using UnityEngine;
using UnityEngine.AI;
using Xiaohai.Character;

public class Enemy : MonoBehaviour
{
    private StateMachine _fsm;

    public string CurrentState;
    private NavMeshAgent _agent;
    private Damageable _damageable;
    private AttackHandler _attackHandler;
    public float DetectRange = 10f;
    public float AttackRange = 1.5f;
    public float AttackDegrees = 30f;


    [SerializeField] private RuntimeTransformAnchor _playerTransform;
    private Damageable _playerDamageable;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _damageable = GetComponent<Damageable>();
        _attackHandler = GetComponent<AttackHandler>();
    }
    void Start()
    {
        _playerDamageable = _playerTransform.Value.GetComponent<Damageable>();

        _fsm = new StateMachine(this);
        _fsm.AddState("Idle");

        _fsm.AddState("FollowPlayer",
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
        });
        _fsm.AddState("Attacking", onLogic: (state) =>
        {
            AttackPlayer();
        });
        _fsm.AddState("Defeat", onEnter: (state) =>
        {
            transform.localScale = Vector3.one * 0.5f;
            var mat = GetComponentInChildren<MeshRenderer>().material;
            mat.color = Color.red;

            var collider = GetComponent<CapsuleCollider>();
            collider.enabled = false;

            _agent.enabled = false;

            Destroy(gameObject, 10);
        });

        _fsm.AddTransitionFromAny(new Transition("", "Defeat", (transition) => _damageable.IsDead));
        _fsm.AddTransition("Idle", "FollowPlayer", (transition) =>
        {
            return IsPlayerAlive() && DistanceToPlayer() <= DetectRange && !IsPlayerVisible();
        });
        _fsm.AddTransition("FollowPlayer", "Idle", (transition) =>
        {
            return DistanceToPlayer() > DetectRange;
        });
        _fsm.AddTransition("Idle", "Attacking", (transition) => IsPlayerAlive() && DistanceToPlayer() <= AttackRange && IsPlayerVisible());
        _fsm.AddTransition("FollowPlayer", "Attacking", (transition) => IsPlayerAlive() && DistanceToPlayer() <= AttackRange && IsPlayerVisible());
        _fsm.AddTransition("Attacking", "FollowPlayer", (transition) => DistanceToPlayer() > AttackRange || !IsPlayerVisible());
        _fsm.AddTransitionFromAny("Idle", t => !IsPlayerAlive());

        _fsm.SetStartState("Idle");
        _fsm.Init();
    }

    float DistanceToPlayer()
    {
        var playerPosition = _playerTransform.Value.transform.position;
        float distance = Vector3.Distance(transform.position, playerPosition);
        return distance;
    }

    /// <summary>
    /// Check if the player is blocked by obstacles
    /// </summary>
    /// <returns>false if it is blocked by other obstacles</returns>
    bool IsPlayerVisible()
    {
        if (Physics.CapsuleCast(transform.position, transform.position + Vector3.up, _agent.radius, transform.forward, out RaycastHit hit))
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
        bool alive = !_playerDamageable.IsDead;
        return alive;
    }

    void MoveTowardsPlayer()
    {
        _agent.SetDestination(_playerTransform.Value.position);
    }

    float _timer;
    public float AttackInterval = 2f;
    void AttackPlayer()
    {
        var playerPosition = _playerTransform.Value.position;
        var direction = playerPosition - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction.normalized, Vector3.up), 4 * Time.deltaTime);
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
    }
}
