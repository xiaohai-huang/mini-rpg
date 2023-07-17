using System;
using UnityEngine;
using UnityEngine.Events;

namespace Xiaohai.Character
{
    public class TargetPicker : MonoBehaviour
    {
        [SerializeField] private LayerMask _targetMask;
        private Character _character;
        public GameObject Target;
        /// <summary>
        /// The first argument is the new target.  
        /// The second argument is the previous target.
        /// </summary>
        public UnityEvent<GameObject, GameObject> OnTargetChanged;
        void Awake()
        {
            _character = GetComponent<Character>();
        }

        // Update is called once per frame
        void Update()
        {
            var newTarget = GetTarget(15f);
            OnTargetChanged?.Invoke(newTarget, Target);
            Target = newTarget;
        }

        GameObject GetTarget(float radius)
        {
            var damageables = _character.GetNearByDamageables(radius, _targetMask);
            if (damageables.Length > 0)
            {
                Damageable target = damageables[0];
                float minDistance = Mathf.Infinity;
                Array.ForEach(damageables, damageable =>
                {
                    var distance = Vector3.Distance(transform.position, damageable.transform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        target = damageable;
                    }
                });
                return target.gameObject;
            }
            return null;
        }
    }
}
