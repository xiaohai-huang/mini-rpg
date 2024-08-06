using Core.Game;
using Core.Game.Statistics;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Xiaohai.Character;
using Xiaohai.Utilities;

namespace Xiaohai.Turret
{
    public class Turret : Entity
    {
        [ReadOnly]
        public Damageable Target;
        public Transform AttackBall;
        public MultiAimConstraint AttackBallAimConstraint;
        public Transform AttackBallTarget;
        public Transform FirePoint;
        public float AttackSpeed;
        public float AttackRange;
        public TurretProjectile BulletPrefab;
        public DamageableTargetSelection TargetSelection;
        public StatSystem Statistics;

        [SerializeField]
        private BaseStats _baseStats;

        void Awake()
        {
            Statistics = new StatSystem(_baseStats);
            AttackSpeed = Statistics.GetStat(StatType.AttackSpeed).ComputedValue;
            AttackRange = Statistics.GetStat(StatType.BasicAttackRange).ComputedValue;
            Init();
        }

        void Init()
        {
            TargetSelection.GetComponent<CapsuleCollider>().radius = AttackRange;
        }

#if UNITY_EDITOR
        void OnDrawGizmosSelected()
        {
            Init();
        }
#endif
    }
}
