using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Xiaohai.Utilities
{
    public class NearByUnits : MonoBehaviour
    {
        [SerializeField]
        private LayerMask _enemyMask;

        [Range(0f, 30f)]
        public float Radius;

        /// <summary>
        /// The number of colliders near the unit.
        /// </summary>
        /// <remarks>
        /// Access `Colliders` before using this field.
        /// </remarks>
        [ReadOnly]
        public int Count;

        [SerializeField]
        [ReadOnly]
        private Collider[] _colliders = new Collider[32];
        public Collider[] Colliders
        {
            get
            {
                Count = Physics.OverlapSphereNonAlloc(transform.position, Radius, _colliders, _enemyMask);
                return _colliders;
            }
        }

        void Update()
        {

        }

        public GameObject GetClosest()
        {
            Collider[] colliders = Colliders;

            if (Count == 0)
                return null;


            float minDistSqr = Radius * Radius;
            Collider closest = colliders[0];

            // begin loop from second element
            for (int i = 1; i < Count; i++)
            {
                float distSqr = (colliders[i].transform.position - transform.position).sqrMagnitude;
                if (distSqr < minDistSqr)
                {
                    minDistSqr = distSqr;
                    closest = colliders[i];
                }
            }

            return closest.gameObject;
        }

#if UNITY_EDITOR
        void OnDrawGizmosSelected()
        {
            Handles.color = Color.yellow;
            Handles.DrawWireDisc(transform.position, Vector3.up, Radius);
        }
#endif
    }
}