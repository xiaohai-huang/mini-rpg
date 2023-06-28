using FSM;
using UnityEngine;

public class DefeatState : State
{
    private readonly PlayerController _mono;
    public DefeatState(PlayerController mono) : base()
    {
        _mono = mono;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _mono.PlayerDefeatEventChannel.RaiseEvent();
    }
}
