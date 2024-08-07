using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xiaohai.Character.Arthur
{
    public class Shield : MonoBehaviour
    {
        public int DamageAmount;

        public void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Damageable damageable))
            {
                damageable.TakeDamage(DamageAmount);
            }
        }
    }
}
