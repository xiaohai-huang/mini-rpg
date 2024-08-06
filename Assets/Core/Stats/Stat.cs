using System.Collections.Generic;

namespace Core.Game.Statistics
{
    public class Stat
    {
        private readonly Dictionary<ModifierType, HashSet<Modifier>> _modifiers = new();
        public float BaseValue { get; set; }
        public float ComputedValue
        {
            get => GetComputedValue();
        }

        private readonly StatSystem _system;

        public Stat(StatSystem system)
        {
            _system = system;
            InitializeModifiers();
        }

        public Stat(float baseValue, StatSystem system)
        {
            BaseValue = baseValue;
            _system = system;
            InitializeModifiers();
        }

        public bool AddModifier(Modifier modifier)
        {
            return _modifiers[modifier.Type].Add(modifier);
        }

        public bool RemoveModifier(Modifier modifier)
        {
            return _modifiers[modifier.Type].Remove(modifier);
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
