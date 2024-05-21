using System.Collections.Generic;
using UnityEngine;

namespace Xiaohai.Character.XiaoQiao
{
    public class Wind : MonoBehaviour
    {
        readonly HashSet<Collider> _victims = new();

        [SerializeField]
        private float _flyingDuration = 1.5f;

        void Start()
        {
            Destroy(gameObject, 0.5f);
        }

        void OnTriggerEnter(Collider other)
        {
            // Hit a new target
            if (!_victims.Contains(other))
            {
                // Apply damage
                if (
                    other.TryGetComponent<Damageable>(out var damageable)
                    && other.TryGetComponent<Enemy>(out var enemy)
                )
                {
                    // Calculate damage
                    damageable.TakeDamage(300);
                    // Launch the target into sky for a given time
                    enemy.KnockUp(_flyingDuration);
                    Debug.Log($"Launch {enemy}", enemy);
                }

                _victims.Add(other);
            }
        }
    }
}
