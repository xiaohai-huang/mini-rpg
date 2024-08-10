using System;
using System.Collections.Generic;
using UnityEngine;

namespace Xiaohai.Character.XiaoQiao
{
    public class Wind : MonoBehaviour
    {
        readonly HashSet<Collider> _victims = new();

        public Action<Enemy> OnHit;

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
                if (other.TryGetComponent<Enemy>(out var enemy))
                {
                    OnHit?.Invoke(enemy);
                }

                _victims.Add(other);
            }
        }
    }
}
