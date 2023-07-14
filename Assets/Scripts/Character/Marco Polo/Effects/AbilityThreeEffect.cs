using UnityEngine;

namespace Xiaohai.Character.MarcoPolo
{
    public class AbilityThreeEffect : Effect
    {
        private readonly int _damage;
        private readonly float _duration;
        private readonly float _radius;
        private readonly float _attackRate;
        private float _timer;
        private float _attackCoolDownTimer;
        private readonly GameObject _circlePrefab;
        private GameObject _circle;
        public AbilityThreeEffect(int damage, float radius, float duration, float attackRate, GameObject effectPrefab)
        {
            Name = "Marco Polo Ability Three";
            _damage = damage;
            _duration = duration;
            _radius = radius;
            _attackRate = attackRate;
            _circlePrefab = effectPrefab;
        }

        public override void OnApply(EffectSystem system)
        {
            _circle = Object.Instantiate(_circlePrefab, system.transform.position, Quaternion.identity, system.transform);
            _circle.transform.localScale = new Vector3(_radius, _circle.transform.localScale.y, _radius);
            _circle.SetActive(true);
        }

        public override void OnUpdate(EffectSystem system)
        {
            if (_attackCoolDownTimer < 0)
            {
                Collider[] colliders = Physics.OverlapSphere(system.transform.position, _radius);
                foreach (Collider collider in colliders)
                {
                    if (!collider.CompareTag("Player") && collider.TryGetComponent(out Damageable damageable))
                    {
                        damageable.TakeDamage(_damage);
                    }
                }
                _attackCoolDownTimer = _attackRate;
            }
            _attackCoolDownTimer -= Time.deltaTime;

            // overall timer
            _timer += Time.deltaTime;
            if (_timer > _duration)
            {
                Finished = true;
            }
        }

        public override void OnRemove(EffectSystem system)
        {
            Object.Destroy(_circle);
        }
    }

}
