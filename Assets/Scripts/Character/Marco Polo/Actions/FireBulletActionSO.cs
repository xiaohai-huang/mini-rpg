using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "FireBulletAction", menuName = "State Machines/Actions/Marco Polo/Fire Bullet Action")]
public class FireBulletActionSO : StateActionSO
{
    protected override StateAction CreateAction() => new FireBulletAction();
}

public class FireBulletAction : StateAction
{
    private MarcoPolo _marcoPolo;
    protected new FireBulletActionSO OriginSO => (FireBulletActionSO)base.OriginSO;

    public override void Awake(StateMachine stateMachine)
    {
        _marcoPolo = stateMachine.GetComponent<MarcoPolo>();
    }

    public override void OnUpdate()
    {
    }

    public override void OnStateEnter()
    {
        _marcoPolo.Attack();
    }

    public override void OnStateExit()
    {
    }
}
