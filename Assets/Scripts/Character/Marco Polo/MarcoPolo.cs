using UnityEngine;

public class MarcoPolo : MonoBehaviour
{
    public Transform LeftGunFirePoint;
    public Transform RightGunFirePoint;
    public GameObject BulletPrefab;

    public float RotateSpeed = 8f;
    public int NumberOfBullets = 20;
    /// <summary>
    /// The number of bullets can be fired in one second.
    /// </summary>
    public float BulletSpawnSpeed = 10f;
    public float Distance = 5f;

    public bool ShouldPerformingAbilityOne;

    private bool _attackHand;
    private Character _character;
    private float _lastFireTime;
    private int _numBulletsFired;
    void Awake()
    {
        _character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ShouldPerformingAbilityOne)
        {
            PerformAbilityOne();
        }
        else
        {
            _lastFireTime = 0;
            _numBulletsFired = 0;
        }
    }

    public void Attack()
    {
        if (_attackHand)
        {
            FireBullet(LeftGunFirePoint);
        }
        else
        {
            FireBullet(RightGunFirePoint);
        }
        _attackHand = !_attackHand;
    }


    GameObject FireBullet(Transform firePoint)
    {
        var bullet = Instantiate(BulletPrefab, firePoint.position, firePoint.rotation);
        var go = bullet.GetComponent<GoForward>();
        go.Speed = 15f;
        go.DamageAmount = Random.Range(200, 500);

        return bullet;
    }

    private bool toggle;

    private void FireBullet()
    {
        FireBullet(toggle ? LeftGunFirePoint : RightGunFirePoint);
        toggle = !toggle;
    }

    public void PerformAbilityOne()
    {
        var direction = new Vector3(_character.AbilityOneDirection.x, 0, _character.AbilityOneDirection.y);

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            _character.transform.rotation = Quaternion.Slerp(_character.transform.rotation, targetRotation, RotateSpeed * Time.deltaTime);
        }

        if (_numBulletsFired >= NumberOfBullets)
        {
            _character.AbilityOneInput = false;
            ShouldPerformingAbilityOne = false;
        }
        else
        {
            if (Time.time > _lastFireTime + 1 / BulletSpawnSpeed)
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

    public void PerformAbilityTwo()
    {
        var direction = new Vector3(_character.AbilityTwoDirection.x, 0, _character.AbilityTwoDirection.y);
        if (direction == Vector3.zero)
        {
            _character.transform.position = _character.transform.position + _character.transform.forward * Distance;
        }
        else
        {
            if (!ShouldPerformingAbilityOne)
            {
                _character.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
            }
            _character.transform.position = _character.transform.position + (direction * Distance);
        }
    }
}
