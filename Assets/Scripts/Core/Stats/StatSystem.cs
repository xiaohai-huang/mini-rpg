using System;
using System.Collections.Generic;

namespace Core.Game.Statistics
{
    public class StatSystem
    {
        private readonly Dictionary<StatType, Stat> Stats = new();
        public event Action<StatType, float> OnStatComputedValueChanged;

        public StatSystem()
        {
            Init();
        }

        private void Init()
        {
            foreach (StatType type in Enum.GetValues(typeof(StatType)))
            {
                Stats[type] = new Stat(this);
                Stats[type].OnComputedValueChanged += newValue =>
                    OnStatComputedValueChanged?.Invoke(type, newValue);
            }
        }

        public StatSystem(BaseStats baseStats)
        {
            foreach (var pair in baseStats)
            {
                StatType type = pair.Key;
                Stats[type] = new Stat(pair.Value, this);
                Stats[type].OnComputedValueChanged += newValue =>
                    OnStatComputedValueChanged?.Invoke(type, newValue);
            }
        }

        public bool AddModifier(Modifier modifier)
        {
            return Stats[modifier.TargetStatType].AddModifier(modifier);
        }

        public bool RemoveModifier(Modifier modifier)
        {
            return Stats[modifier.TargetStatType].RemoveModifier(modifier);
        }

        public void SetStatBaseValue(StatType type, float baseValue)
        {
            Stats[type].BaseValue = baseValue;
        }

        public Stat GetStat(StatType statType)
        {
            return Stats[statType];
        }
    }
}
