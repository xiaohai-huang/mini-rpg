namespace Core.Game.Statistics
{
    public enum StatType
    {
        MaxHealth = 0,
        MovementSpeed,
        PhysicalDamage,
        MagicalDamage,
        Armor,
        MagicalResistance,
        AttackSpeed,
        Tenacity
    }

    public enum ModifierType
    {
        Float,
        Percentage,
        Special
    }

    public abstract class Modifier
    {
        public StatType TargetStatType { private set; get; }
        public ModifierType Type { protected set; get; }

        public Modifier(StatType targetStatType)
        {
            TargetStatType = targetStatType;
        }

        public abstract float Apply(float prevValue, StatSystem system);
    }

    // (1 + amount) * prev value
    public class PercentageModifier : Modifier
    {
        public float Amount;

        public PercentageModifier(StatType statType, float amount)
            : base(statType)
        {
            Type = ModifierType.Percentage;
            Amount = amount;
        }

        public override float Apply(float prevValue, StatSystem system)
        {
            return (1 + Amount) * prevValue;
        }
    }

    // prev value + amount
    public class FloatModifier : Modifier
    {
        public float Amount;

        public FloatModifier(StatType statType, float amount)
            : base(statType)
        {
            Type = ModifierType.Float;
            Amount = amount;
        }

        public override float Apply(float prevValue, StatSystem system)
        {
            return prevValue + Amount;
        }
    }
}
