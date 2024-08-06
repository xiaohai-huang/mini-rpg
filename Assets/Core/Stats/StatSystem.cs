using System.Collections.Generic;
using DotNetSystem = System;

namespace Core.Game.Statistics
{
    public class StatSystem
    {
        private readonly Dictionary<StatType, Stat> Stats = new();

        public StatSystem()
        {
            Init();
        }

        private void Init()
        {
            foreach (StatType type in DotNetSystem.Enum.GetValues(typeof(StatType)))
            {
                Stats[type] = new Stat(this);
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

        public Stat GetStat(StatType statType)
        {
            return Stats[statType];
        }
    }
}
