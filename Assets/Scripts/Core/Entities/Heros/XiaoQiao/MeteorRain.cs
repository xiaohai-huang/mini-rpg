using System;
using System.Collections.Generic;
using Core.Game.Entities;
using UnityEngine;
using Xiaohai.Utilities;

namespace Xiaohai.Character.XiaoQiao
{
    public class MeteorRain : MonoBehaviour
    {
        [SerializeField]
        private DamageableTargetSelection _targetSelection;

        [SerializeField]
        private float _duration;

        [SerializeField]
        private float _attackInterval = 0.3f;

        [SerializeField]
        private int _maxAttacksPerEntity = 4;

        [SerializeField]
        private float _radius;

        [SerializeField]
        private Transform _circle;

        [SerializeField]
        private Meteor _meteor;

        // enemy1: 2 hits
        // enemy2: 3 hits
        readonly Dictionary<Damageable, int> _victims = new();

        // Attack cool down
        float _timer;

        public Action<Base, float> OnHit;

        void Start()
        {
            Destroy(gameObject, _duration);
        }

        // Update is called once per frame
        void Update()
        {
            _timer -= Time.deltaTime;

            if (_timer < 0f)
            {
                // pick a target
                var enemy = _targetSelection.GetTarget(
                    DamageableTargetSelection.SelectionStrategy.Random
                );
                if (enemy == null)
                    return;

                if (Attack(enemy))
                {
                    _timer = _attackInterval;
                }
            }
        }

        void UpdateSize()
        {
            var newLocalScale = _circle.localScale;
            newLocalScale.x = _radius * 2;
            newLocalScale.y = _radius * 2;

            _circle.localScale = newLocalScale;
        }

        Meteor CreateMeteor(Damageable target)
        {
            var meteor = Instantiate(
                _meteor,
                target.transform.position + Vector3.up * 15f,
                Quaternion.identity
            );

            return meteor;
        }

        bool Attack(Damageable enemy)
        {
            if (_victims.TryGetValue(enemy, out int numAttacks))
            {
                if (numAttacks >= _maxAttacksPerEntity)
                    return false;
            }

            var meteor = CreateMeteor(enemy);
            meteor.OnHit += () =>
            {
                OnHit?.Invoke(enemy.GetComponent<Base>(), numAttacks == 0 ? 1 : 0.5f);
            };
            meteor.FlyTowards(enemy);
            _victims[enemy] = numAttacks + 1;
            return true;
        }

#if UNITY_EDITOR
        void OnDrawGizmosSelected()
        {
            if (_circle == null)
                return;
            UpdateSize();
        }
#endif
    }
}
