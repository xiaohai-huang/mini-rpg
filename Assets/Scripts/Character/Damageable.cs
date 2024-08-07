using Core.Game.Entities;
using Core.Game.Statistics;
using UnityEngine;
using UnityEngine.Events;

namespace Xiaohai.Character
{
    [RequireComponent(typeof(Base))]
    public class Damageable : MonoBehaviour
    {
        private Health _health;
        public int CurrentHealth => _health.CurrentHealth;
        public int MaxHealth => _health.MaxHealth;

        [Header("Broadcasting On")]
        public UnityEvent<int, int> OnHealthChanged;
        public UnityEvent<int> OnTakenDamage;
        public UnityEvent<int> OnRestoreHealth;
        public UnityEvent OnDie;

        public bool IsDead;
        private Stat _maxHealthStat;

        void Start()
        {
            _maxHealthStat = GetComponent<Base>().Statistics.GetStat(StatType.MaxHealth);
            if (!TryGetComponent(out _health))
            {
                _health = gameObject.AddComponent<Health>();
                _health.MaxHealth = (int)_maxHealthStat.ComputedValue;
                _health.CurrentHealth = (int)_maxHealthStat.ComputedValue;
            }
            FireOnHealthChangedEvent();
        }

        void OnEnable()
        {
            _maxHealthStat.OnComputedValueChanged += OnMaxHealthChanged;
        }

        void OnDisable()
        {
            _maxHealthStat.OnComputedValueChanged -= OnMaxHealthChanged;
        }

        private void OnMaxHealthChanged(float newValue)
        {
            _health.MaxHealth = (int)newValue;
        }

        void FireOnHealthChangedEvent()
        {
            OnHealthChanged?.Invoke(_health.CurrentHealth, _health.MaxHealth);
        }

        public void TakeDamage(int amount)
        {
            if (IsDead)
                return;
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
            if (IsDead)
                return;

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
