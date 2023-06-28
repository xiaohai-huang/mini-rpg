using FSM;
using UnityEngine;
using Xiaohai.Input;

public class PlayerController : MonoBehaviour
{
    private StateMachine fsm;
    public string CurrentState;
    public InputReader InputReader;
    private HP _hp;
    [Header("Broadcasting on")]
    public VoidEventChannel PlayerDefeatEventChannel;

    [Header("Listening on")]
    public VoidEventChannel PlayerResurrectEventChannel;

    [HideInInspector] public float WalkSpeed = 5f;
    [HideInInspector] public float RotateSpeed = 8f;
    void Awake()
    {
        _hp = GetComponent<HP>();
    }
    void Start()
    {
        fsm = new StateMachine();
        fsm.AddState("Idle", new IdleState());
        fsm.AddState("Walk", new WalkState(this));
        fsm.AddState("Attack", new AttackState(this));
        fsm.AddState("Defeat", new DefeatState(this));
        fsm.AddState("Resurrection", onEnter: (s) =>
        {
            _hp.Resurrect();
            s.fsm.RequestStateChange("Idle");
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
        fsm.AddTransitionFromAny("Defeat", (t) => _hp.CurrentHP == 0);

        fsm.SetStartState("Idle");
        fsm.Init();

        // user input
        InputReader.OnAttack += () =>
        {
            fsm.Trigger("OnAttack");
        };

        // event
        PlayerResurrectEventChannel.OnEventRaised += () => fsm.Trigger("Resurrect");
    }

    // Update is called once per frame
    void Update()
    {
        fsm.OnLogic();
        CurrentState = fsm.ActiveStateName;
    }
}
