using System;
using Core.Game.Combat;
using Core.Game.Entities;
using Core.Game.Statistics;
using UnityEngine;
using UnityEngine.Events;

namespace Xiaohai.Character
{
    [RequireComponent(typeof(Base))]
    public class Damageable : MonoBehaviour
    {
        public bool Invincible;
        public int CurrentHealth { get; private set; }
        public int MaxHealth { get; private set; }

        [Header("Broadcasting On")]
        public UnityEvent<int, int> OnHealthChanged;
        public UnityEvent<int> OnTakenDamage;
        public UnityEvent<int> OnRestoreHealth;
        public UnityEvent OnDie;

        public bool IsDead;
        private Stat _maxHealthStat;
        private Base _base;

        void Awake()
        {
            _base = GetComponent<Base>();
        }

        bool _started = false;

        void Start()
        {
            _maxHealthStat = _base.Statistics.GetStat(StatType.MaxHealth);

            MaxHealth = (int)_maxHealthStat.ComputedValue;
            CurrentHealth = (int)_maxHealthStat.ComputedValue;
            FireOnHealthChangedEvent();
            RegisterCallbacks();
            _started = true;
        }

        void RegisterCallbacks()
        {
            _maxHealthStat.OnComputedValueChanged += OnMaxHealthChanged;
        }

        void UnregisterCallbacks()
        {
            _maxHealthStat.OnComputedValueChanged -= OnMaxHealthChanged;
        }

        void OnEnable()
        {
            if (_started)
            {
                RegisterCallbacks();
            }
        }

        void OnDisable()
        {
            UnregisterCallbacks();
        }

        private void OnMaxHealthChanged(float newValue)
        {
            MaxHealth = (int)newValue;
        }

        void FireOnHealthChangedEvent()
        {
            OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
        }

        public void TakeDamage(int amount)
        {
            if (Invincible)
                return;
            if (IsDead)
                return;
            ReduceHealth(amount);

            FireOnHealthChangedEvent();
            OnTakenDamage?.Invoke(amount);

            if (CurrentHealth <= 0)
            {
                IsDead = true;
                OnDie?.Invoke();
            }
        }

        public void TakeDamage(Damage damage)
        {
            TakeDamage((int)damage.ComputedValue);
        }

        public void RestoreHealth(int healthToAdd)
        {
            if (IsDead)
                return;

            int increasedHealth = IncreaseHealth(healthToAdd);
            if (increasedHealth == 0)
                return;

            FireOnHealthChangedEvent();
            OnRestoreHealth?.Invoke(increasedHealth);
        }

        public void Kill()
        {
            TakeDamage(CurrentHealth);
        }

        public void Resurrect()
        {
            IsDead = false;
            RestoreHealth(MaxHealth);
        }

        private void ReduceHealth(int healthAmount)
        {
            if (CurrentHealth == 0)
                return;
            CurrentHealth = Math.Max(CurrentHealth - healthAmount, 0);
        }

        /// <summary>
        /// Increase the current health.
        /// </summary>
        /// <param name="healthAmount"></param>
        /// <returns>The amount of hp that is being increased.</returns>
        private int IncreaseHealth(int healthAmount)
        {
            int originalHealth = CurrentHealth;
            CurrentHealth = Math.Clamp(CurrentHealth + healthAmount, 0, MaxHealth);
            int diff = CurrentHealth - originalHealth;

            return diff;
        }
    }
}
