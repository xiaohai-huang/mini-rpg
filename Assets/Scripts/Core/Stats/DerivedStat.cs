using System;
using Core.Game.Common;

namespace Core.Game.Statistics
{
    public class PercentagePhysicalResistance : Stat
    {
        private readonly Stat _physicalResistanceStat;

        public PercentagePhysicalResistance(StatSystem system)
            : base(system)
        {
            _physicalResistanceStat = system.GetStat(StatType.PhysicalResistance);
            _physicalResistanceStat.OnComputedValueChanged += (_) =>
            {
                UpdateComputedValue();
            };
            UpdateComputedValue();
        }

        protected override void OnAfterInit() { }

        protected override float GetComputedValue()
        {
            float physicalResistance = _physicalResistanceStat.ComputedValue;
            return physicalResistance / (physicalResistance + Constants.DAMAGE_FORMULA_COEFFICIENT);
        }

        public PercentagePhysicalResistance(float baseValue, StatSystem system)
            : base(baseValue, system)
        {
            if (baseValue != 0)
                throw new ArgumentException(
                    "This is a derived stat. It does not accept `baseValue`."
                );
        }

        public override bool AddModifier(Modifier modifier)
        {
            throw new NotImplementedException("Derived stat does not support this method.");
        }

        public override bool RemoveModifier(Modifier modifier)
        {
            throw new NotImplementedException("Derived stat does not support this method.");
        }
    }

    public class PercentageMagicalResistance : Stat
    {
        private readonly Stat _magicalResistanceStat;

        public PercentageMagicalResistance(StatSystem system)
            : base(system)
        {
            _magicalResistanceStat = system.GetStat(StatType.MagicalResistance);
            _magicalResistanceStat.OnComputedValueChanged += (_) =>
            {
                UpdateComputedValue();
            };
            UpdateComputedValue();
        }

        protected override void OnAfterInit() { }

        protected override float GetComputedValue()
        {
            float magicalResistance = _magicalResistanceStat.ComputedValue;
            return magicalResistance / (magicalResistance + Constants.DAMAGE_FORMULA_COEFFICIENT);
        }

        public PercentageMagicalResistance(float baseValue, StatSystem system)
            : base(baseValue, system)
        {
            if (baseValue != 0)
                throw new ArgumentException(
                    "This is a derived stat. It does not accept `baseValue`."
                );
        }

        public override bool AddModifier(Modifier modifier)
        {
            throw new NotImplementedException("Derived stat does not support this method.");
        }

        public override bool RemoveModifier(Modifier modifier)
        {
            throw new NotImplementedException("Derived stat does not support this method.");
        }
    }
}
