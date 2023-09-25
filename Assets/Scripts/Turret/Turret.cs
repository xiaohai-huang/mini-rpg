using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Xiaohai.Turret
{
    public class Turret : MonoBehaviour
    {
        public Transform AttackBall;
        public MultiAimConstraint AttackBallAimConstraint;
        public Transform AttackBallTarget;
        public Transform FirePoint;
        public float AttackSpeed;
        public float AttackRange;
        public TurretProjectile BulletPrefab;
    }
}
