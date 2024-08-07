using System;
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
        private Character _character;
        private GameObject _circle;
        private LayerMask _targetMask = LayerMask.GetMask("Enemy");

        public AbilityThreeEffect(
            int damage,
            float radius,
            float duration,
            float attackRate,
            GameObject effectPrefab
        )
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
            base.OnApply(system);
            _character = system.GetComponent<Character>();
            _circle = UnityEngine.Object.Instantiate(
                _circlePrefab,
                system.transform.position,
                Quaternion.identity,
                system.transform
            );
            _circle.transform.localScale = new Vector3(
                _radius * 2,
                _circle.transform.localScale.y,
                _radius * 2
            );
            _circle.SetActive(true);
        }

        public override void OnUpdate(EffectSystem system)
        {
            base.OnUpdate(system);
            if (_attackCoolDownTimer < 0)
            {
                Damageable[] damageables = _character.GetNearByDamageables(_radius, _targetMask);
                Array.ForEach(damageables, damageable => damageable.TakeDamage(_damage));
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
            base.OnRemove(system);
            UnityEngine.Object.Destroy(_circle);
        }
    }
}
