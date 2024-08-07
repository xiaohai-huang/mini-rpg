using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(
    fileName = "SayTextAction",
    menuName = "State Machines/Actions/Test/Say Text Action"
)]
public class SayTextActionSO : StateActionSO
{
    public string Text;

    protected override StateAction CreateAction() => new SayTextAction();
}

public class SayTextAction : StateAction
{
    protected new SayTextActionSO OriginSO => (SayTextActionSO)base.OriginSO;

    public override void Awake(StateMachine stateMachine)
    {
        Debug.Log($"Instantiate {this}, with {OriginSO.name}");
    }

    public override void OnUpdate() { }

    public override void OnStateEnter()
    {
        Debug.Log(OriginSO.Text);
    }

    public override void OnStateExit() { }
}
