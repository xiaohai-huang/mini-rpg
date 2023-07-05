using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "AbilityOneAction", menuName = "State Machines/Actions/Marco Polo/Ability One Action")]
public class AbilityOneActionSO : StateActionSO
{
    public float RotateSpeed = 8f;
    public int NumberOfBullets = 10;
    /// <summary>
    /// The number of bullets can be fired in one second.
    /// </summary>
    public float BulletSpawnSpeed = 10f;
    protected override StateAction CreateAction() => new AbilityOneAction();
}

public class AbilityOneAction : StateAction
{
    private Character _character;
    private MarcoPolo _polo;
    private float _lastFireTime;
    private int _numBulletsFired;
    protected new AbilityOneActionSO OriginSO => (AbilityOneActionSO)base.OriginSO;

    public override void Awake(StateMachine stateMachine)
    {
        _character = stateMachine.GetComponent<Character>();
        _polo = stateMachine.GetComponent<MarcoPolo>();
    }

    public override void OnUpdate()
    {
        var direction = new Vector3(_character.AbilityOneDirection.x, 0, _character.AbilityOneDirection.y);

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            _character.transform.rotation = Quaternion.Slerp(_character.transform.rotation, targetRotation, OriginSO.RotateSpeed * Time.deltaTime);
        }

        if (_numBulletsFired >= OriginSO.NumberOfBullets)
        {
            _character.AbilityOneInput = false;
        }
        else
        {
            if (Time.time > _lastFireTime + 1 / OriginSO.BulletSpawnSpeed)
            {
                float angles = Vector3.Angle(_character.transform.forward, direction);
                if (angles < 1.0f)
                {
                    FireBullet();
                    _lastFireTime = Time.time;
                    _numBulletsFired++;

                }
            }
        }



    }

    public override void OnStateEnter()
    {

    }

    public override void OnStateExit()
    {
        _lastFireTime = 0;
        _numBulletsFired = 0;
    }

    private bool toggle;
    private void FireBullet()
    {
        FireBullet(toggle ? _polo.LeftGunFirePoint : _polo.RightGunFirePoint);
        toggle = !toggle;
    }

    GameObject FireBullet(Transform firePoint)
    {
        var bullet = Object.Instantiate(_polo.BulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<GoForward>().Speed = 10f;
        return bullet;
    }
}
