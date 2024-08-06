using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xiaohai.Character;

namespace Core.Game.Entities.Turrets
{
    public class TurretProjectile : MonoBehaviour
    {
        public float Speed;
        public int DamageAmount = 10;
        public float Threshold = 0.3f;
        public Vector3 Offset;
        private Damageable _target;

        // Start is called before the first frame update
        void Start()
        {
            Destroy(gameObject, 10f);
        }

        // Update is called once per frame
        void Update()
        {
            if (_target != null)
            {
                Vector3 destination = _target.transform.position + Offset;
                float dist = Vector3.Distance(transform.position, destination);
                bool reached = dist < Threshold;
                if (reached)
                {
                    _target.TakeDamage(DamageAmount);
                    Destroy(gameObject);
                }
                else
                {
                    transform.LookAt(_target.transform);
                    transform.position = Vector3.MoveTowards(
                        transform.position,
                        destination,
                        Speed * Time.deltaTime
                    );
                }
            }
        }

        public void SetTarget(Damageable target)
        {
            _target = target;
        }
    }
}
