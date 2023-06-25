using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;
public class IdleState : State
{
    public IdleState() : base()
    {
    }

    public override void OnEnter()
    {
        Debug.Log("Enter the idle state");
    }
}
