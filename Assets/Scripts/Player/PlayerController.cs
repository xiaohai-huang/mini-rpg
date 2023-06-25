using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

public class PlayerController : MonoBehaviour
{
    private StateMachine fsm;
    public string CurrentState;
    [HideInInspector] public CharacterMovement CharacterMovement;
    // Start is called before the first frame update
    void Start()
    {
        CharacterMovement = GetComponent<CharacterMovement>();
        Debug.Log("Run start in player controller");
        fsm = new StateMachine(this);
        fsm.AddState("Idle", new IdleState());
        fsm.AddState("Walk", new WalkState(this));

        fsm.AddTransition(new Transition("Idle", "Walk", t =>
        {
            return (Input.GetMouseButtonDown(0));
            
        }));

        fsm.AddTransition(new Transition("Walk", "Idle", t =>
        {
            return !CharacterMovement.Moving;
        }));

        fsm.SetStartState("Idle");
        fsm.Init();
    }

    // Update is called once per frame
    void Update()
    {
        fsm.OnLogic();
        CurrentState = fsm.ActiveStateName;
    }
}
