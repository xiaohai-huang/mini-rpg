using FSM;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private StateMachine _fsm;

    public string CurrentState;
    private NavMeshAgent _agent;
    private HP _hp;
    private AttackHandler _attackHandler;
    public float DetectRange = 10f;
    public float AttackRange = 1.5f;
    public float AttackDegrees = 30f;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _hp = GetComponent<HP>();
        _attackHandler = GetComponent<AttackHandler>();
    }
    void Start()
    {
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
        _fsm.AddState("Death", onEnter: (state) =>
        {
            transform.localScale = Vector3.one * 0.5f;
            var mat = GetComponentInChildren<MeshRenderer>().material;
            mat.color = Color.red;

            Destroy(gameObject, 10);
        });

        _fsm.AddTransitionFromAny(new Transition("", "Death", (transition) => _hp.CurrentHP == 0));
        _fsm.AddTransition("Idle", "FollowPlayer", (transition) =>
        {
            return DistanceToPlayer() <= DetectRange;
        });
        _fsm.AddTransition("FollowPlayer", "Idle", (transition) =>
        {
            return DistanceToPlayer() > DetectRange;
        });
        _fsm.AddTransition("Idle", "Attacking", (transition) => DistanceToPlayer() <= AttackRange);
        _fsm.AddTransition("FollowPlayer", "Attacking", (transition) => DistanceToPlayer() <= AttackRange);

        _fsm.AddTransition(new Transition("Attacking", "FollowPlayer", (transition) => DistanceToPlayer() > AttackRange));
        _fsm.SetStartState("Idle");
        _fsm.Init();
    }

    float DistanceToPlayer()
    {
        var playerPosition = GameManager.Instance.Player.transform.position;
        float distance = Vector3.Distance(transform.position, playerPosition);
        return distance;
    }

    void MoveTowardsPlayer()
    {
        _agent.SetDestination(GameManager.Instance.Player.transform.position);
    }

    float _timer;
    public float AttackInterval = 2f;
    void AttackPlayer()
    {
        var playerPosition = GameManager.Instance.Player.transform.position;
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
