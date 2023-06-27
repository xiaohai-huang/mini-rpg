using UnityEngine;
using FSM;
using Xiaohai.Input;

public class PlayerController : MonoBehaviour
{
    private StateMachine fsm;
    public string CurrentState;
    public InputReader InputReader;

    [HideInInspector] public float WalkSpeed = 5f;
    [HideInInspector] public float RotateSpeed = 8f;
    void Start()
    {
        fsm = new StateMachine(this);
        fsm.AddState("Idle", new IdleState());
        fsm.AddState("Walk", new WalkState(this));
        fsm.AddState("Attack",new AttackState(this));

        fsm.AddTransition(new Transition("Idle", "Walk", t =>
        {
            return InputReader.Move != Vector2.zero;
        }));

        fsm.AddTransition(new Transition("Walk", "Idle", t =>
        {
            return InputReader.Move == Vector2.zero;
        }));

        fsm.AddTriggerTransitionFromAny("OnAttack", new Transition("", "Attack"));

        fsm.SetStartState("Idle");
        fsm.Init();

        // user input
        InputReader.OnAttack += () => 
        {
            fsm.Trigger("OnAttack");
        };
    }

    // Update is called once per frame
    void Update()
    {
        fsm.OnLogic();
        CurrentState = fsm.ActiveStateName;
    }
}
