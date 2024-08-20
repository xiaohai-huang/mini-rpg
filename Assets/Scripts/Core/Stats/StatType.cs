namespace Core.Game.Statistics
{
    public enum StatType
    {
        MaxHealth = 0,
        MovementSpeed,
        PhysicalDamage,
        MagicalDamage,
        PhysicalResistance,
        MagicalResistance,
        AttackSpeed,
        Tenacity,
        BasicAttackRange,
        ViewRange,
        FlatPhysicalResistancePenetration,
        FlatMagicalResistancePenetration,
        PercentagePhysicalResistancePenetration,
        PercentageMagicalResistancePenetration,

        // Derived stat
        PercentagePhysicalResistance,

        // Derived stat
        PercentageMagicalResistance,
        MaxMana,
        ManaRecoveredPerSecond,
        HealthRecoveredPerSecond
    }
}
