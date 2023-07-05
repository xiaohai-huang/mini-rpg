using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "HorizontalMoveAction", menuName = "State Machines/Actions/Horizontal Move Action")]
public class HorizontalMoveActionSO : StateActionSO
{
    public float WalkSpeed = 8f;
    protected override StateAction CreateAction() => new HorizontalMoveAction();
}

public class HorizontalMoveAction : StateAction
{
    private Character _character;
    protected new HorizontalMoveActionSO OriginSO => (HorizontalMoveActionSO)base.OriginSO;

    public override void Awake(StateMachine stateMachine)
    {
        _character = stateMachine.GetComponent<Character>();
    }

    public override void OnUpdate()
    {
        _character.Velocity.x = OriginSO.WalkSpeed * _character.HorizontalInput.x;
        _character.Velocity.z = OriginSO.WalkSpeed * _character.HorizontalInput.y;
    }
}
