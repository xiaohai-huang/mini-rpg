using System;
using System.Collections.Generic;

namespace Core.Game.Statistics
{
    public class Stat
    {
        private readonly Dictionary<ModifierType, HashSet<Modifier>> _modifiers = new();
        private float _baseValue;
        public float BaseValue
        {
            get => _baseValue;
            set
            {
                _baseValue = value;
                UpdateComputedValue();
            }
        }
        private float _prevComputedValue;
        public float ComputedValue { get; private set; }

        public event Action<float> OnComputedValueChanged;

        private readonly StatSystem _system;

        public Stat(StatSystem system)
        {
            InitializeModifiers();
            _system = system;
            UpdateComputedValue();
        }

        public Stat(float baseValue, StatSystem system)
        {
            InitializeModifiers();
            BaseValue = baseValue;
            _system = system;
            UpdateComputedValue();
        }

        public bool AddModifier(Modifier modifier)
        {
            var success = _modifiers[modifier.Type].Add(modifier);
            UpdateComputedValue();
            return success;
        }

        public bool RemoveModifier(Modifier modifier)
        {
            var success = _modifiers[modifier.Type].Remove(modifier);
            ComputedValue = GetComputedValue();
            UpdateComputedValue();
            return success;
        }

        void UpdateComputedValue()
        {
            ComputedValue = GetComputedValue();
            if (_prevComputedValue != ComputedValue)
            {
                _prevComputedValue = ComputedValue;
                OnComputedValueChanged?.Invoke(ComputedValue);
            }
        }

        private float GetComputedValue()
        {
            // BaseValue -> float modifiers -> percentage modifiers -> special modifiers
            float computed = BaseValue;

            ApplyModifiers(_modifiers[ModifierType.Float], ref computed);
            ApplyModifiers(_modifiers[ModifierType.Percentage], ref computed);
            ApplyModifiers(_modifiers[ModifierType.Special], ref computed);

            return computed;
        }

        private void ApplyModifiers(IEnumerable<Modifier> modifiers, ref float value)
        {
            foreach (var modifier in modifiers)
            {
                value = modifier.Apply(value, _system);
            }
        }

        private void InitializeModifiers()
        {
            _modifiers.Add(ModifierType.Float, new HashSet<Modifier>());
            _modifiers.Add(ModifierType.Percentage, new HashSet<Modifier>());
            _modifiers.Add(ModifierType.Special, new HashSet<Modifier>());
        }
    }
}
