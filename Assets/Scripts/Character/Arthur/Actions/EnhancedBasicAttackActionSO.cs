using System.Threading.Tasks;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using Xiaohai.Utilities;

namespace Xiaohai.Character.Arthur
{
    [CreateAssetMenu(
        fileName = "Enhanced Basic Attack Action",
        menuName = "State Machines/Actions/Arthur/Enhanced Basic Attack Action"
    )]
    public class EnhancedBasicAttackActionSO : StateActionSO
    {
        /// <summary>
        /// The amount of time in ms it takes to finish the attack.
        /// </summary>
        public float AttackTime = 500.0f;
        public float AttackRange = 5f;
        public AbilityOneEffectSO AbilityOneEffectSO;

        protected override StateAction CreateAction() => new EnhancedBasicAttackAction();
    }

    public class EnhancedBasicAttackAction : StateAction
    {
        private Character _character;
        private CharacterController _characterController;
        private EffectSystem _effectSystem;
        private Animator _animator;
        private static readonly int BASIC_ATTACK_ANIMATION_ID = Animator.StringToHash(
            "Basic Attack"
        );
        private static readonly int ABILITY_ONE_SPEED_UP_ANIMATION_ID = Animator.StringToHash(
            "Ability One Speed Up"
        );
        private static readonly int ABILITY_ONE_FLYING_ANIMATION_ID = Animator.StringToHash(
            "Ability One Flying"
        );

        protected new EnhancedBasicAttackActionSO OriginSO =>
            (EnhancedBasicAttackActionSO)base.OriginSO;

        public override void Awake(StateMachine stateMachine)
        {
            _character = stateMachine.GetComponent<Character>();
            _characterController = stateMachine.GetComponent<CharacterController>();
            _effectSystem = stateMachine.GetComponent<EffectSystem>();
            _animator = stateMachine.GetComponent<Animator>();
        }

        public override void OnUpdate() { }

        private const float DESTINATION_OFFSET = 1.25f;
        private const float FLYING_SPEED = 25f;

        public override async void OnStateEnter()
        {
            _character.BasicAttackInput = false;
            _effectSystem.RemoveEffect(OriginSO.AbilityOneEffectSO);
            _animator.SetBool(ABILITY_ONE_SPEED_UP_ANIMATION_ID, false);

            _character.PerformingBasicAttack = true;
            var target = _character.Target;
            _animator.SetBool(ABILITY_ONE_FLYING_ANIMATION_ID, true);
            if (target != null)
            {
                // face towards the target
                Vector3 direction = (
                    target.transform.position - _character.transform.position
                ).normalized;
                while (!(Vector3.Dot(_character.transform.forward, direction) > 0.99f))
                {
                    var targetRotation = Quaternion.LookRotation(direction, Vector3.up);
                    _character.transform.rotation = Quaternion.Slerp(
                        _character.transform.rotation,
                        targetRotation,
                        15f * Time.deltaTime
                    );

                    await Task.Yield();
                }
                // leap towards the target
                Vector3 destination = Navigation.GetLocationWithOffset(
                    target.transform.position,
                    _character.transform.position,
                    DESTINATION_OFFSET
                );
                _characterController.enabled = false;

                while (_character.transform.position != destination)
                {
                    _character.transform.position = Vector3.MoveTowards(
                        _character.transform.position,
                        destination,
                        Time.deltaTime * FLYING_SPEED
                    );
                    await Task.Yield();
                }

                _characterController.enabled = true;

                // deal damage to the target
                target.TakeDamage(180);
            }
            _animator.SetBool(ABILITY_ONE_FLYING_ANIMATION_ID, false);
            _animator.SetTrigger(BASIC_ATTACK_ANIMATION_ID);
            Timer.Instance.SetTimeout(
                () =>
                {
                    _character.PerformingBasicAttack = false;
                },
                OriginSO.AttackTime
            );
        }

        public override void OnStateExit() { }
    }
}
