using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using Xiaohai.Character;

[CreateAssetMenu(fileName = "HorizontalMoveAction", menuName = "State Machines/Actions/Horizontal Move Action")]
public class HorizontalMoveActionSO : StateActionSO
{
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
        _character.WalkSpeed = _character.BaseWalkSpeed + _character.BonusWalkSpeed;
        _character.Velocity.x = _character.WalkSpeed * _character.HorizontalInput.x;
        _character.Velocity.z = _character.WalkSpeed * _character.HorizontalInput.y;
    }
}
