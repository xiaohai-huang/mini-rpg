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
                Stats[type] = CreateStat(type);
            }
        }

        public StatSystem(BaseStats baseStats)
        {
            foreach (var pair in baseStats)
            {
                StatType type = pair.Key;
                float baseValue = pair.Value;
                Stats[type] = CreateStat(type, baseValue);
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

        private Stat CreateStat(StatType type)
        {
            Stat newStat = type switch
            {
                StatType.PercentagePhysicalResistance => new PercentagePhysicalResistance(this),
                StatType.PercentageMagicalResistance => new PercentageMagicalResistance(this),
                _ => new Stat(this),
            };
            newStat.OnComputedValueChanged += newValue =>
                OnStatComputedValueChanged?.Invoke(type, newValue);
            return newStat;
        }

        private Stat CreateStat(StatType type, float baseValue)
        {
            Stat newStat = CreateStat(type);
            newStat.BaseValue = baseValue;
            return newStat;
        }
    }
}
