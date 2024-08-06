using UnityEngine;

namespace Xiaohai.Character.XiaoQiao
{
    public class Meteor : MonoBehaviour
    {
        [SerializeField]
        private float _speed;
        private Damageable _target;
        private int _damageAmount;

        public void Attack(Damageable target, int damageAmount)
        {
            _target = target;
            _damageAmount = damageAmount;
        }

        void Update()
        {
            if (_target == null)
                return;
            transform.LookAt(_target.transform);
            // move towards the target
            transform.position = Vector3.MoveTowards(
                transform.position,
                _target.transform.position,
                _speed * Time.deltaTime
            );

            if (Vector3.Distance(transform.position, _target.transform.position) < 0.3f)
            {
                // Apply damage to the target
                _target.TakeDamage(_damageAmount);
                Destroy(gameObject);
            }
        }
    }
}
