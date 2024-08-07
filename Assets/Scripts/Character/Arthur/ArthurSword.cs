using UnityEngine;

namespace Xiaohai.Character.Arthur
{
    public class ArthurSword : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Damageable damageable))
            {
                damageable.TakeDamage(Random.Range(500, 800));
            }
        }
    }
}
