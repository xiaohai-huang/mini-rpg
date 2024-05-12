using UnityHFSM;
using UnityEngine;
using Xiaohai.Character;
using Xiaohai.Input;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private StateMachine fsm;
    public string CurrentState;
    public InputReader InputReader;
    private Damageable _damageable;

    public float WalkSpeed = 5f;
    public float RotateSpeed = 12f;
    void Awake()
    {
        _damageable = GetComponent<Damageable>();
    }
    void Start()
    {
        fsm = new StateMachine();
        fsm.AddState("Idle", new IdleState());
        fsm.AddState("Walk", new WalkState(this));
        fsm.AddState("Attack", new AttackState(this));
        fsm.AddState("Defeat", new DefeatState());
        fsm.AddState("Resurrection", onEnter: (s) =>
        {
            _damageable.Resurrect();
            fsm.RequestStateChange("Idle");
        });

        fsm.AddTransition(new Transition("Idle", "Walk", t =>
        {
            return InputReader.Move != Vector2.zero;
        }));

        fsm.AddTransition(new Transition("Walk", "Idle", t =>
        {
            return InputReader.Move == Vector2.zero;
        }));

        fsm.AddTriggerTransition("OnAttack", new Transition("Idle", "Attack"));
        fsm.AddTriggerTransition("OnAttack", new Transition("Walk", "Attack"));
        fsm.AddTriggerTransition("Resurrect", new Transition("Defeat", "Resurrection"));
        fsm.AddTransitionFromAny("Defeat", (t) => _damageable.IsDead);

        fsm.SetStartState("Idle");
        fsm.Init();

        // user input
        InputReader.OnAttack += () =>
        {
            fsm.Trigger("OnAttack");
        };
    }

    public void Trigger(string trigger)
    {
        fsm.Trigger(trigger);
    }

    // Update is called once per frame
    void Update()
    {
        fsm.OnLogic();
        CurrentState = fsm.ActiveStateName;
    }
}
