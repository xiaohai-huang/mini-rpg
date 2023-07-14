using System.Collections;
using UnityEngine;
using Xiaohai.Character.MarcoPolo;

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
    [Header("Ability 1")]
    public bool ShouldPerformingAbilityOne;


    [Header("Ability 2")]
    public float AbilityTwoDashDistance = 5f;

    [Header("Ability 3")]
    public float AbilityThreeDashDistance = 8f;
    public float AbilityThreeSpinDuration = 3f;
    public float AbilityThreeSpinMoveSpeed = 3f;
    public float AbilityThreeEffectRadius = 6f;
    public float AbilityThreeAttackRate = 0.3f;
    public int AbilityThreeDamage = 300;
    public GameObject AbilityThreeEffectPrefab;


    private bool _attackHand;
    private Character _character;
    private EffectSystem _effectSystem;
    private CharacterController _characterController;
    private Animator _animator;
    private static readonly int ABILITY_THREE_ANIMATION_ID = Animator.StringToHash("Ability Three");
    private float _lastFireTime;
    private int _numBulletsFired;
    void Awake()
    {
        _character = GetComponent<Character>();
        _characterController = GetComponent<CharacterController>();
        _effectSystem = GetComponent<EffectSystem>();
        _animator = GetComponent<Animator>();
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
        _characterController.enabled = false;
        var direction = new Vector3(_character.AbilityTwoDirection.x, 0, _character.AbilityTwoDirection.y);
        if (direction == Vector3.zero)
        {
            _character.transform.position = _character.transform.position + _character.transform.forward * AbilityTwoDashDistance;
        }
        else
        {
            if (!ShouldPerformingAbilityOne)
            {
                _character.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
            }
            _character.transform.position = _character.transform.position + (direction * AbilityTwoDashDistance);
        }
        _characterController.enabled = true;
    }

    private Coroutine _abilityThreeCoroutine;
    private AbilityThreeEffect _abilityThreeEffect;
    public void PerformAbilityThree()
    {
        // 1. dash along to the direction instructed by _character.AbilityThreeDirection for `AbilityThreeDashDistance` distance
        // 2. spin for `AbilityThreeSpinDuration` seconds and deal damage to enemies, neutral objects within the circle of `AbilityThreeEffectRadius` radius
        // while spinning, move towards the direction of _character.AbilityThreeDirection with the speed of `AbilityThreeSpinMoveSpeed`

        _characterController.enabled = false;

        var direction = new Vector3(_character.AbilityThreeDirection.x, 0, _character.AbilityThreeDirection.y);
        if (direction == Vector3.zero)
        {
            _character.transform.position = _character.transform.position + _character.transform.forward * AbilityThreeDashDistance;
        }
        else
        {
            _character.transform.SetPositionAndRotation(_character.transform.position + (direction * AbilityThreeDashDistance), Quaternion.LookRotation(direction, Vector3.up));
        }
        _animator.SetBool(ABILITY_THREE_ANIMATION_ID, true);
        _abilityThreeEffect = new AbilityThreeEffect(AbilityThreeDamage, AbilityThreeEffectRadius, AbilityThreeSpinDuration, AbilityThreeAttackRate, AbilityThreeEffectPrefab);
        _effectSystem.AddEffect(_abilityThreeEffect);
        _characterController.enabled = true;

        _abilityThreeCoroutine = StartCoroutine(MoveForward());
    }

    public void CancelAbilityThree()
    {
        StopCoroutine(_abilityThreeCoroutine);
        _effectSystem.RemoveEffect(_abilityThreeEffect);
        _animator.SetBool(ABILITY_THREE_ANIMATION_ID, false);
    }

    IEnumerator MoveForward()
    {

        float timer = AbilityThreeSpinDuration;
        while (timer > 0)
        {
            _character.Velocity = _character.transform.forward * AbilityThreeSpinMoveSpeed;

            timer -= Time.deltaTime;
            yield return null;
        }

    }
}
