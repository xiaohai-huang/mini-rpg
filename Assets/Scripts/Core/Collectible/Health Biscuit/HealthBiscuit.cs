using UnityEngine;
using Xiaohai.Character;

namespace Core.Game
{
    public class HealthBiscuit : MonoBehaviour
    {
        [SerializeField]
        private int Amount = 500;

        void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Damageable>(out var damageable))
            {
                // Do nothing if the character is full health
                if (damageable.CurrentHealth == damageable.MaxHealth)
                    return;

                damageable.RestoreHealth(Amount);
                Destroy(gameObject);
            }
        }
    }
}
