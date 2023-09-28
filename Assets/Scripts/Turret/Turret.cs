using UnityEngine;
using UnityEngine.Animations.Rigging;
using Xiaohai.Character;
using Xiaohai.Utilities;

namespace Xiaohai.Turret
{
    public class Turret : MonoBehaviour
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

        void Awake()
        {
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
