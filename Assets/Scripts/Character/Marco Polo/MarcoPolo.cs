using System.Collections;
using UnityEngine;

namespace Xiaohai.Character.MarcoPolo
{
    public class MarcoPolo : Character
    {
        public Transform LeftGunFirePoint;
        public Transform RightGunFirePoint;
        public GameObject BulletPrefab;
        public GameObject FollowBulletPrefab;

        public float RotateSpeed = 8f;

        [Header("Basic Attack")]
        public float AttackRange = 7f;
        /// <summary>
        /// The number of attacks per second.
        /// </summary>
        public float BasicAttackSpeed = 0.7f;
        public bool CanDoBasicAttack => _basicAttackCoolDownTimer <= 0f;
        private float _basicAttackCoolDownTimer;
        [Header("Ability 1")]
        /// <summary>
        /// The number of bullets can be fired in one second.
        /// </summary>
        public float BulletSpawnSpeed = 10f;
        public int NumberOfBullets = 20;


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
        private TargetPicker _targetPicker;
        private EffectSystem _effectSystem;
        private CharacterController _characterController;
        private Animator _animator;
        private static readonly int ABILITY_ONE_ANIMATION_ID = Animator.StringToHash("Ability One");
        private static readonly int ABILITY_THREE_ANIMATION_ID = Animator.StringToHash("Ability Three");
        public override void Awake()
        {
            base.Awake();
            _character = GetComponent<Character>();
            _characterController = GetComponent<CharacterController>();
            _effectSystem = GetComponent<EffectSystem>();
            _targetPicker = GetComponent<TargetPicker>();
            _animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        public override void Update()
        {
            base.Update();
            if (_basicAttackCoolDownTimer > 0f)
            {
                _basicAttackCoolDownTimer -= Time.deltaTime;
            }
        }
        public void BasicAttack()
        {
            _basicAttackCoolDownTimer = BasicAttackSpeed;
            if (_targetPicker.Target == null)
            {
                FireBullet(_attackHand ? LeftGunFirePoint : RightGunFirePoint);
            }
            else
            {
                StartCoroutine(BasicAttack(_targetPicker.Target.transform, 20f, _attackHand ? LeftGunFirePoint : RightGunFirePoint));
            }
            _attackHand = !_attackHand;
        }

        IEnumerator BasicAttack(Transform target, float rotateSpeed, Transform firePoint)
        {
            var direction = (target.position - transform.position).normalized;
            while (!(Vector3.Dot(transform.forward, direction) > 0.99f))
            {
                var targetRotation = Quaternion.LookRotation(direction, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
                direction = (target.position - transform.position).normalized;
                yield return null;
            }

            FireFollowBullet(firePoint);
        }

        GameObject FireFollowBullet(Transform firePoint)
        {
            var bullet = Instantiate(FollowBulletPrefab, firePoint.position, firePoint.rotation);
            var follow = bullet.GetComponent<FollowTarget>();
            follow.Target = _targetPicker.Target.transform;
            follow.Speed = 15f;
            follow.Offset = new Vector3(0, 0.5f, 0);
            follow.DamageAmount = Random.Range(200, 500);

            return bullet;
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

        public void FireBullet()
        {
            FireBullet(toggle ? LeftGunFirePoint : RightGunFirePoint);
            toggle = !toggle;
        }

        // Marco Polo gains a 10% increase in movement speed during the continuous shooting process in the specified direction. Each shot causes 150 (+17% physical bonus) physical damage
        // When his attack speed reaches 0/75%/150%, the number of bullets fired is 5/7/9.
        private Coroutine _abilityOneCoroutine;
        public void PerformAbilityOne()
        {
            _character.PerformingAbilityOne = true;

            var direction = new Vector3(_character.AbilityOneDirection.x, 0, _character.AbilityOneDirection.y);

            _abilityOneCoroutine = StartCoroutine(PerformAbilityOneCoroutine(direction));
        }
        private AbilityOneEffect _abilityOneEffect;
        private IEnumerator PerformAbilityOneCoroutine(Vector3 direction)
        {
            // rotate towards the direction
            if (direction != Vector3.zero)
            {
                float angles = Vector3.Angle(_character.transform.forward, direction);
                while (angles > 1.0f)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
                    _character.transform.rotation = Quaternion.Slerp(_character.transform.rotation, targetRotation, RotateSpeed * Time.deltaTime);

                    angles = Vector3.Angle(_character.transform.forward, direction);
                    yield return null;
                }
            }

            // add continuous shoot effect
            _animator.SetBool(ABILITY_ONE_ANIMATION_ID, true);
            _abilityOneEffect = new AbilityOneEffect(NumberOfBullets, BulletSpawnSpeed);
            _abilityOneEffect.OnRemoveCallback += () =>
            {
                _animator.SetBool(ABILITY_ONE_ANIMATION_ID, false);
                _character.PerformingAbilityOne = false;
            };
            _effectSystem.AddEffect(_abilityOneEffect);
        }
        public void CancelAbilityOne()
        {
            StopCoroutine(_abilityOneCoroutine);
            _effectSystem.RemoveEffect(_abilityOneEffect);
        }

        public void PerformAbilityTwo()
        {
            _character.PerformingAbilityTwo = true;
            _characterController.enabled = false;
            var direction = new Vector3(_character.AbilityTwoDirection.x, 0, _character.AbilityTwoDirection.y);
            if (direction == Vector3.zero)
            {
                _character.transform.position = _character.transform.position + _character.transform.forward * AbilityTwoDashDistance;
            }
            else
            {
                // if not performing ability one, face towards the direction
                if (!_character.IsAbilityPerforming(Character.Ability.One))
                {
                    _character.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
                }
                _character.transform.position = _character.transform.position + (direction * AbilityTwoDashDistance);
            }
            _characterController.enabled = true;
            _character.PerformingAbilityTwo = false;
        }

        private Coroutine _abilityThreeCoroutine;
        private AbilityThreeEffect _abilityThreeEffect;
        public void PerformAbilityThree()
        {
            // 1. dash along to the direction instructed by _character.AbilityThreeDirection for `AbilityThreeDashDistance` distance
            // 2. spin for `AbilityThreeSpinDuration` seconds and deal damage to enemies, neutral objects within the circle of `AbilityThreeEffectRadius` radius
            // while spinning, move towards the direction of _character.AbilityThreeDirection with the speed of `AbilityThreeSpinMoveSpeed`
            _character.PerformingAbilityThree = true;
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
            _abilityThreeEffect.OnRemoveCallback += () =>
            {
                _animator.SetBool(ABILITY_THREE_ANIMATION_ID, false);
                _character.PerformingAbilityThree = false;
            };
            _effectSystem.AddEffect(_abilityThreeEffect);
            _characterController.enabled = true;

            _abilityThreeCoroutine = StartCoroutine(MoveForward());
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

        public void CancelAbilityThree()
        {
            StopCoroutine(_abilityThreeCoroutine);
            _effectSystem.RemoveEffect(_abilityThreeEffect);
        }
    }
}
