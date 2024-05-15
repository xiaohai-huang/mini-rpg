using System.Collections.Generic;
using UnityEngine;
using UnityHFSM;

namespace Xiaohai.Character.XiaoQiao
{
    public class Fan : MonoBehaviour
    {
        public float Distance = 10f;
        public float DamageAmount = 200f;

        /// <summary>
        /// Fan flying speed
        /// </summary>
        public float Speed = 2f;

        private Transform _receiver;
        private Vector3 _destination;
        StateMachine sm;
        const float TOLERANCE = 0.5F;
        readonly HashSet<Collider> _victims = new();
        private int _hits = 0;
        private int _damage;
        private const float DAMAGE_DECAY_RATE = 0.2F;
        private const float MIN_DAMAGE_REDUCTION_RATE = 0.5F;

        public void SetReceiver(Transform receiver)
        {
            _receiver = receiver;
        }

        public void Throw(int damage)
        {
            _damage = damage;
            // calculate the destination
            _destination = transform.position + transform.forward * Distance;
            sm.RequestStateChange("FlyingForwards");
        }

        void Awake()
        {
            sm = new StateMachine();
            sm.AddState("Idle", onEnter: (_) => { });
            sm.AddState(
                "FlyingForwards",
                onEnter: (_) =>
                {
                    _hits = 0;
                    _victims.Clear();
                },
                onLogic: (_) =>
                {
                    transform.position = Vector3.Lerp(
                        transform.position,
                        _destination,
                        Speed * Time.deltaTime
                    );

                    if (Vector3.Distance(transform.position, _destination) < TOLERANCE)
                    {
                        sm.RequestStateChange("FlyingBack");
                    }
                }
            );

            sm.AddState(
                "FlyingBack",
                onEnter: (_) =>
                {
                    _victims.Clear();
                },
                onLogic: (_) =>
                {
                    transform.position = Vector3.Lerp(
                        transform.position,
                        _receiver.position,
                        1.5f * Speed * Time.deltaTime
                    );

                    if (Vector3.Distance(transform.position, _receiver.position) < TOLERANCE * 3)
                    {
                        sm.RequestStateChange("Destroyed");
                    }
                }
            );

            sm.AddState(
                "Destroyed",
                onEnter: (_) =>
                {
                    Destroy(gameObject);
                }
            );

            sm.SetStartState("Idle");

            sm.Init();
        }

        // Update is called once per frame
        void Update()
        {
            sm.OnLogic();
        }

        void OnTriggerEnter(Collider other)
        {
            // if (sm.ActiveStateName == "Idle") return;

            // Hit a new target
            if (!_victims.Contains(other))
            {
                // Apply damage
                if (other.TryGetComponent<Damageable>(out var enemy))
                {
                    // Calculate damage
                    float reductionAmount = Mathf.Max(
                        MIN_DAMAGE_REDUCTION_RATE,
                        1f - (_hits * DAMAGE_DECAY_RATE)
                    );

                    enemy.TakeDamage((int)(_damage * reductionAmount));
                }

                _hits++;
                _victims.Add(other);
            }
        }
    }
}
