using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using Xiaohai.Character;

[CreateAssetMenu(fileName = "RotateAction", menuName = "State Machines/Actions/Rotate Action")]
public class RotateActionSO : StateActionSO
{
    public float RotateSpeed = 8f;
    protected override StateAction CreateAction() => new RotateAction();
}

public class RotateAction : StateAction
{
    private Character _character;
    private Transform _tf;
    protected new RotateActionSO OriginSO => (RotateActionSO)base.OriginSO;

    public override void Awake(StateMachine stateMachine)
    {
        _character = stateMachine.GetComponent<Character>();
        _tf = _character.gameObject.transform;
    }

    public override void OnUpdate()
    {
        var inputDirection = new Vector3(_character.HorizontalInput.x, 0, _character.HorizontalInput.y);
        if (inputDirection == Vector3.zero) return;
        Quaternion targetRotation = Quaternion.LookRotation(inputDirection, Vector3.up);
        _tf.rotation = Quaternion.Slerp(_tf.rotation, targetRotation, OriginSO.RotateSpeed * Time.deltaTime);
    }

    public override void OnStateEnter()
    {
    }

    public override void OnStateExit()
    {
    }
}
