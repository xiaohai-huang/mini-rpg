using UnityEngine;
using UnityEngine.Events;

namespace Xiaohai.Character
{
    public class Damageable : MonoBehaviour
    {
        [SerializeField] private HealthConfig _healthConfig;
        private Health _health;

        [Header("Broadcasting On")]
        public UnityEvent<int, int> OnHealthChanged;
        public UnityEvent<int> OnTakenDamage;
        public UnityEvent<int> OnRestoreHealth;
        public UnityEvent OnDie;

        public bool IsDead;
        void Awake()
        {
            if (!TryGetComponent(out _health))
            {
                _health = gameObject.AddComponent<Health>();
                _health.MaxHealth = _healthConfig.MaxHealth;
                _health.CurrentHealth = _healthConfig.MaxHealth;
            }
        }

        void Start()
        {
            FireOnHealthChangedEvent();
        }

        void FireOnHealthChangedEvent()
        {
            OnHealthChanged?.Invoke(_health.CurrentHealth, _health.MaxHealth);
        }

        public void TakeDamage(int amount)
        {
            if (IsDead) return;
            _health.ReduceHealth(amount);

            FireOnHealthChangedEvent();
            OnTakenDamage?.Invoke(amount);

            if (_health.CurrentHealth <= 0)
            {
                IsDead = true;
                OnDie?.Invoke();
            }
        }

        public void RestoreHealth(int healthToAdd)
        {
            if (IsDead) return;

            _health.IncreaseHealth(healthToAdd);

            FireOnHealthChangedEvent();
            OnRestoreHealth?.Invoke(healthToAdd);
        }

        public void Kill()
        {
            TakeDamage(_health.CurrentHealth);
        }

        public void Resurrect()
        {
            IsDead = false;
            RestoreHealth(_health.MaxHealth);
        }
    }
}
