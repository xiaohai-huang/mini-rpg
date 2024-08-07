using System;
using System.Collections.Generic;
using Core.Game.Entities;
using Core.Game.Statistics;
using UnityEngine;
using UnityEngine.AI;
using Xiaohai.Utilities;

namespace Xiaohai.Character
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Character : Base
    {
        [ReadOnly]
        public Damageable Target;

        /// <summary>
        /// Final walk speed.
        /// </summary>
        public float MaxMoveSpeed => Statistics.GetStat(StatType.MovementSpeed).ComputedValue;
        public Vector3 Velocity;
        public Vector2 HorizontalInput;
        public Vector2 HorizontalAutoInput;
        public bool IsAutoMove;

        [Header("Basic Attack")]
        public bool BasicAttackInput;

        [SerializeField]
        private bool _performingBasicAttack;
        public bool PerformingBasicAttack
        {
            get { return _performingBasicAttack; }
            set
            {
                _performingBasicAttack = value;
                OnPerformingBasicAttackChanged?.Invoke(value);
            }
        }
        public Action<bool> OnPerformingBasicAttackChanged;

        [Header("Ability One")]
        public bool AbilityOneInput;
        public bool PerformingAbilityOne;
        public Vector2 AbilityOnePosition;
        public Vector2 AbilityOneDirection;

        [Header("Ability Two")]
        public bool AbilityTwoInput;
        public bool PerformingAbilityTwo;
        public Vector2 AbilityTwoPosition;
        public Vector2 AbilityTwoDirection;

        [Header("Ability Three")]
        public bool AbilityThreeInput;
        public bool PerformingAbilityThree;
        public Vector2 AbilityThreePosition;
        public Vector2 AbilityThreeDirection;
        private NavMeshAgent _navMeshAgent;
        public DamageableTargetSelection TargetSelection { get; private set; }

        public enum Ability
        {
            Basic,
            One,
            Two,
            Three,
            Four
        }

        public bool HasAbilityInput(Ability ability)
        {
            switch (ability)
            {
                case Ability.Basic:
                    return BasicAttackInput;
                case Ability.One:
                    return AbilityOneInput;
                case Ability.Two:
                    return AbilityTwoInput;
                case Ability.Three:
                    return AbilityThreeInput;
                case Ability.Four:
                    break;
            }
            return false;
        }

        public bool IsAbilityPerforming(Ability ability)
        {
            switch (ability)
            {
                case Ability.Basic:
                    return PerformingBasicAttack;
                case Ability.One:
                    return PerformingAbilityOne;
                case Ability.Two:
                    return PerformingAbilityTwo;
                case Ability.Three:
                    return PerformingAbilityThree;
                case Ability.Four:
                    break;
            }
            return false;
        }

        public void SetAbilityInput(Ability ability, bool value)
        {
            switch (ability)
            {
                case Ability.Basic:
                    BasicAttackInput = value;
                    break;
                case Ability.One:
                    AbilityOneInput = value;
                    break;
                case Ability.Two:
                    AbilityTwoInput = value;
                    break;
                case Ability.Three:
                    AbilityThreeInput = value;
                    break;
                case Ability.Four:
                    break;
            }
        }

        public override void Awake()
        {
            base.Awake();
            _navMeshAgent = GetComponent<NavMeshAgent>();

            // Sync the range of the character with target selection's selection range
            TargetSelection = GetComponentInChildren<DamageableTargetSelection>();
            TargetSelection.GetComponent<CapsuleCollider>().radius = Statistics
                .GetStat(StatType.ViewRange)
                .ComputedValue;
        }

        public virtual void Update()
        {
            _navMeshAgent.speed = MaxMoveSpeed;
            Target = TargetSelection.GetTarget(DamageableTargetSelection.SelectionStrategy.Closest);
        }

        private readonly Collider[] colliders = new Collider[20];

        public Damageable[] GetNearByDamageables(float radius)
        {
            int numColliders = Physics.OverlapSphereNonAlloc(transform.position, radius, colliders);
            List<Damageable> damageables = new();
            for (int i = 0; i < numColliders; i++)
            {
                var collider = colliders[i];
                if (
                    !collider.CompareTag("Player")
                    && collider.TryGetComponent(out Damageable damageable)
                )
                {
                    damageables.Add(damageable);
                }
            }
            return damageables.ToArray();
        }

        private readonly Collider[] colliders2 = new Collider[20];

        public Damageable[] GetNearByDamageables(float radius, LayerMask layerMask)
        {
            int numColliders = Physics.OverlapSphereNonAlloc(
                transform.position,
                radius,
                colliders2,
                layerMask
            );
            Damageable[] damageables = new Damageable[numColliders];
            for (int i = 0; i < numColliders; i++)
            {
                var collider = colliders2[i];
                if (
                    !collider.CompareTag("Player")
                    && collider.TryGetComponent(out Damageable damageable)
                )
                {
                    damageables[i] = damageable;
                }
            }
            return damageables;
        }
    }
}
