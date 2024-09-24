using System;
using Core.Game.Entities;
using Core.Game.Statistics;
using UnityEngine;
using UnityEngine.Events;

namespace Core.Game.Mana
{
    public class ManaSystem : MonoBehaviour
    {
        public int CurrentMana { get; private set; }
        public int MaxMana { get; private set; }
        public bool ZeroCooldown;

        [Header("Broadcasting On")]
        public UnityEvent<int, int> OnChange;
        public UnityEvent<int> OnConsume;
        public UnityEvent<int> OnRestore;

        private Stat _maxManaStat;

        private Base _base;

        void Awake()
        {
            _base = GetComponent<Base>();
        }

        bool _started;

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

        void Start()
        {
            _maxManaStat = _base.Statistics.GetStat(StatType.MaxMana);
            MaxMana = (int)_maxManaStat.ComputedValue;
            CurrentMana = (int)_maxManaStat.ComputedValue;
            FireOnManaChangedEvent();
            RegisterCallbacks();
            _started = true;
        }

        void RegisterCallbacks()
        {
            _maxManaStat.OnComputedValueChanged += OnMaxManaChanged;
        }

        void UnregisterCallbacks()
        {
            _maxManaStat.OnComputedValueChanged -= OnMaxManaChanged;
        }

        void OnMaxManaChanged(float newMaxMana)
        {
            MaxMana = (int)_maxManaStat.ComputedValue;
        }

        void FireOnManaChangedEvent()
        {
            OnChange?.Invoke(CurrentMana, MaxMana);
        }

        public void RecoverMana(int amount)
        {
            if (amount <= 0)
                return;
            int originalMana = CurrentMana;
            CurrentMana = Math.Clamp(CurrentMana + amount, 0, MaxMana);
            int recoveredAmount = CurrentMana - originalMana;

            if (recoveredAmount == 0)
                return;
            FireOnManaChangedEvent();
            OnRestore?.Invoke(recoveredAmount);
        }

        public bool Consume(int amount)
        {
            if (ZeroCooldown)
                return true;

            if (amount == 0)
                return true;

            if (CurrentMana < amount)
            {
                return false;
            }

            CurrentMana -= amount;
            FireOnManaChangedEvent();
            OnRestore?.Invoke(amount);
            return true;
        }
    }
}
