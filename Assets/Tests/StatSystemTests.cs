using Core.Game.Common;
using Core.Game.Statistics;
using NUnit.Framework;

public class StatSystemTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void FloatModifier01()
    {
        var system = new StatSystem();
        var floatModifier = new FloatModifier(StatType.MaxHealth, 200);
        system.AddModifier(floatModifier);

        float result = system.GetStat(StatType.MaxHealth).ComputedValue;
        Assert.That(result, Is.EqualTo(200));
    }

    [Test]
    public void FloatModifier02()
    {
        var system = new StatSystem();
        var maxHealth = system.GetStat(StatType.MaxHealth);

        var floatModifier = new FloatModifier(StatType.MaxHealth, 200);
        var floatModifier2 = new FloatModifier(StatType.MaxHealth, 400);
        system.AddModifier(floatModifier);
        Assert.That(maxHealth.ComputedValue, Is.EqualTo(200));
        system.AddModifier(floatModifier2);
        Assert.That(maxHealth.ComputedValue, Is.EqualTo(600));
    }

    [Test]
    public void ModifyStatWithFloatModifier()
    {
        var system = new StatSystem();
        var healthStat = system.GetStat(StatType.MaxHealth);

        var floatModifier = new FloatModifier(StatType.MaxHealth, 100);
        system.AddModifier(floatModifier);

        Assert.That(healthStat.ComputedValue, Is.EqualTo(100)); // After applying the modifier, the stat value should match the modifier's value

        system.RemoveModifier(floatModifier);
        Assert.That(healthStat.ComputedValue, Is.EqualTo(0)); // After removal, the stat value should revert to its original value
    }

    [Test]
    public void ApplyDifferentTypesOfModifiers()
    {
        var system = new StatSystem();
        var maxHealthStat = system.GetStat(StatType.MaxHealth);
        maxHealthStat.BaseValue = 100;
        var PhysicalDamageStat = system.GetStat(StatType.PhysicalDamage);
        PhysicalDamageStat.BaseValue = 10;

        var floatModifierMaxHealth = new FloatModifier(StatType.MaxHealth, 50);
        var percentageModifierPhysicalDamage = new PercentageModifier(
            StatType.PhysicalDamage,
            0.25f
        );

        system.AddModifier(floatModifierMaxHealth);
        system.AddModifier(percentageModifierPhysicalDamage);
        Assert.That(maxHealthStat.ComputedValue, Is.EqualTo(150)); // Base Max Health + 50
        Assert.That(PhysicalDamageStat.ComputedValue, Is.EqualTo(12.5f)); // Base Attack Damage * 1.25
    }

    [Test]
    public void OverlappingModifiersApplySequentially()
    {
        var system = new StatSystem();
        var maxHealthStat = system.GetStat(StatType.MaxHealth);

        var floatModifier1 = new FloatModifier(StatType.MaxHealth, 50);
        var floatModifier2 = new FloatModifier(StatType.MaxHealth, 30);

        system.AddModifier(floatModifier1);
        system.AddModifier(floatModifier2);

        Assert.That(maxHealthStat.ComputedValue, Is.EqualTo(80)); // 50 + 30
    }

    [Test]
    public void CumulativeEffectOfMultipleModifiers()
    {
        var system = new StatSystem();
        var maxHealthStat = system.GetStat(StatType.MaxHealth);
        maxHealthStat.BaseValue = 100;

        var floatModifier1 = new FloatModifier(StatType.MaxHealth, 50);
        var floatModifier2 = new FloatModifier(StatType.MaxHealth, 30);
        var percentageModifier = new PercentageModifier(StatType.MaxHealth, 0.25f);

        system.AddModifier(floatModifier1);
        system.AddModifier(floatModifier2);
        system.AddModifier(percentageModifier);

        Assert.That(maxHealthStat.ComputedValue, Is.EqualTo(225f)); // (100 (base) + 50 + 30) * (1 + 0.25)
    }

    [Test]
    public void ReapplyingModifiersMaintainsCumulativeEffect()
    {
        var system = new StatSystem();
        var maxHealthStat = system.GetStat(StatType.MaxHealth);
        maxHealthStat.BaseValue = 100;
        var floatModifier = new FloatModifier(StatType.MaxHealth, 50);
        system.AddModifier(floatModifier);

        // Reapply the modifier
        system.AddModifier(floatModifier);

        Assert.That(maxHealthStat.ComputedValue, Is.EqualTo(150)); // Should remain at 100 + 50, not double to 100 + 50 + 50
    }

    [Test]
    public void OrderOfApplyingModifiersMatters()
    {
        var system = new StatSystem();
        var maxHealthStat = system.GetStat(StatType.MaxHealth);
        maxHealthStat.BaseValue = 100f;

        var floatModifier = new FloatModifier(StatType.MaxHealth, 50);
        var percentageModifier = new PercentageModifier(StatType.MaxHealth, 0.25f);

        system.AddModifier(floatModifier);
        Assert.That(maxHealthStat.ComputedValue, Is.EqualTo(150)); // First, apply float modifier

        system.AddModifier(percentageModifier);
        Assert.That(maxHealthStat.ComputedValue, Is.EqualTo(187.5f)); // Then, apply percentage modifier
    }

    [Test]
    public void InteractionBetweenDifferentStats()
    {
        var system = new StatSystem();
        var maxHealthStat = system.GetStat(StatType.MaxHealth);
        maxHealthStat.BaseValue = 100; // Initialize BaseValue
        var PhysicalDamageStat = system.GetStat(StatType.PhysicalDamage);
        PhysicalDamageStat.BaseValue = 10; // Initialize BaseValue

        var floatModifierMaxHealth = new FloatModifier(StatType.MaxHealth, 50);

        system.AddModifier(floatModifierMaxHealth);

        Assert.That(maxHealthStat.ComputedValue, Is.EqualTo(150)); // Base Max Health + 50
        Assert.That(PhysicalDamageStat.ComputedValue, Is.EqualTo(10)); // Base Physical Damage remains unchanged
    }

    [Test]
    public void SetBaseValuesForDifferentStats()
    {
        var system = new StatSystem();

        system.SetStatBaseValue(StatType.MaxHealth, 100);
        system.SetStatBaseValue(StatType.PhysicalDamage, 10);

        Assert.That(system.GetStat(StatType.MaxHealth).BaseValue, Is.EqualTo(100));
        Assert.That(system.GetStat(StatType.PhysicalDamage).BaseValue, Is.EqualTo(10));
    }

    [Test]
    public void PhysicalResistance_ShouldCalculateCorrectPercentage()
    {
        var system = new StatSystem();
        system.SetStatBaseValue(StatType.PhysicalResistance, 150);

        var result = system.GetStat(StatType.PercentagePhysicalResistance).ComputedValue;
        Assert.That(result, Is.EqualTo(0.2f).Within(Constants.TOLERANCE));
    }

    [Test]
    public void PhysicalResistance_WithModifier_ShouldUpdatePercentage()
    {
        var system = new StatSystem();
        system.SetStatBaseValue(StatType.PhysicalResistance, 150);
        var initialResult = system.GetStat(StatType.PercentagePhysicalResistance).ComputedValue;
        Assert.That(initialResult, Is.EqualTo(0.2f).Within(Constants.TOLERANCE));

        var add90 = new FloatModifier(StatType.PhysicalResistance, 90);
        system.AddModifier(add90);
        var updatedResult = system.GetStat(StatType.PercentagePhysicalResistance).ComputedValue;
        Assert.That(updatedResult, Is.EqualTo(0.285f).Within(Constants.TOLERANCE));
    }

    [Test]
    public void PhysicalResistance_WithModifier_ThenRemoval_ShouldRevertToOriginalPercentage()
    {
        var system = new StatSystem();
        system.SetStatBaseValue(StatType.PhysicalResistance, 150);

        var initialResult = system.GetStat(StatType.PercentagePhysicalResistance).ComputedValue;
        Assert.That(initialResult, Is.EqualTo(0.2f).Within(Constants.TOLERANCE));

        var add90 = new FloatModifier(StatType.PhysicalResistance, 90);
        system.AddModifier(add90);
        var modifiedResult = system.GetStat(StatType.PercentagePhysicalResistance).ComputedValue;
        Assert.That(modifiedResult, Is.EqualTo(0.285f).Within(Constants.TOLERANCE));

        system.RemoveModifier(add90);
        var revertedResult = system.GetStat(StatType.PercentagePhysicalResistance).ComputedValue;
        Assert.That(revertedResult, Is.EqualTo(0.2f).Within(Constants.TOLERANCE));
    }

    [Test]
    public void PuttingOnPhysicalShoes_ShouldUpdateResistance()
    {
        var system = new StatSystem();
        system.SetStatBaseValue(StatType.PhysicalResistance, 150);
        var initialResult = system.GetStat(StatType.PercentagePhysicalResistance).ComputedValue;
        Assert.That(initialResult, Is.EqualTo(0.2f).Within(Constants.TOLERANCE));

        var add120 = new FloatModifier(StatType.PhysicalResistance, 120);
        system.AddModifier(add120);
        var updatedResult = system.GetStat(StatType.PercentagePhysicalResistance).ComputedValue;
        Assert.That(updatedResult, Is.EqualTo(0.31f).Within(Constants.TOLERANCE));

        Assert.That(
            system.GetStat(StatType.PhysicalResistance).ModifiersEffect,
            Is.EqualTo(120f).Within(Constants.TOLERANCE)
        );
    }

    [Test]
    public void InitialStat_ShouldHaveNoModifiers()
    {
        var system = new StatSystem();

        system.SetStatBaseValue(StatType.PhysicalResistance, 100);
        var stat = system.GetStat(StatType.PhysicalResistance);
        Assert.That(stat.ModifiersEffect, Is.EqualTo(0f).Within(Constants.TOLERANCE));
    }

    [Test]
    public void AddingMultipleModifiers_ShouldCalculateCorrectPercentage()
    {
        var system = new StatSystem();

        system.SetStatBaseValue(StatType.PhysicalResistance, 150);
        var add120 = new FloatModifier(StatType.PhysicalResistance, 120);
        var add90 = new FloatModifier(StatType.PhysicalResistance, 90);
        system.AddModifier(add120);
        system.AddModifier(add90);
        var percentStat = system.GetStat(StatType.PercentagePhysicalResistance);

        Assert.That(percentStat.ComputedValue, Is.EqualTo(0.375f).Within(Constants.TOLERANCE));
        Assert.That(
            system.GetStat(StatType.PhysicalResistance).ModifiersEffect,
            Is.EqualTo(210f).Within(Constants.TOLERANCE)
        );
    }

    [Test]
    public void RemovingAllModifiers_ShouldRevertToBasePercentage()
    {
        var system = new StatSystem();

        system.SetStatBaseValue(StatType.PhysicalResistance, 150);
        var add50 = new FloatModifier(StatType.PhysicalResistance, 50);
        var add30 = new FloatModifier(StatType.PhysicalResistance, 30);
        system.AddModifier(add50);
        system.AddModifier(add30);
        system.RemoveModifier(add50);
        system.RemoveModifier(add30);
        var result = system.GetStat(StatType.PercentagePhysicalResistance).ComputedValue;
        Assert.That(result, Is.EqualTo(0.2f).Within(Constants.TOLERANCE));
    }

    [Test]
    public void MagicalResistance_ShouldCalculateCorrectPercentage()
    {
        var system = new StatSystem();

        system.SetStatBaseValue(StatType.MagicalResistance, 75);
        var result = system.GetStat(StatType.PercentageMagicalResistance).ComputedValue;
        Assert.That(result, Is.EqualTo(0.111f).Within(Constants.TOLERANCE));
    }

    [Test]
    public void MixResistances_90MR_210PR()
    {
        var system = new StatSystem();

        system.SetStatBaseValue(StatType.MagicalResistance, 75);
        system.SetStatBaseValue(StatType.PhysicalResistance, 150);

        var PR = system.GetStat(StatType.PhysicalResistance);
        var p_PR = system.GetStat(StatType.PercentagePhysicalResistance);
        var MR = system.GetStat(StatType.MagicalResistance);
        var p_MR = system.GetStat(StatType.PercentageMagicalResistance);

        var add90_MR = new FloatModifier(StatType.MagicalResistance, 90);
        var add120_PR = new FloatModifier(StatType.PhysicalResistance, 120);
        var add90_PR = new FloatModifier(StatType.PhysicalResistance, 90);
        system.AddModifier(add90_MR);
        system.AddModifier(add120_PR);
        system.AddModifier(add90_PR);

        Assert.That(p_MR.ComputedValue, Is.EqualTo(0.215f).Within(Constants.TOLERANCE));
        Assert.That(MR.ModifiersEffect, Is.EqualTo(90).Within(Constants.TOLERANCE));

        Assert.That(p_PR.ComputedValue, Is.EqualTo(0.375f).Within(Constants.TOLERANCE));
        Assert.That(PR.ModifiersEffect, Is.EqualTo(210).Within(Constants.TOLERANCE));

        system.RemoveModifier(add90_MR);
        Assert.That(p_MR.ComputedValue, Is.EqualTo(0.111f).Within(Constants.TOLERANCE));
        Assert.That(MR.ModifiersEffect, Is.EqualTo(0).Within(Constants.TOLERANCE));

        system.RemoveModifier(add90_PR);
        Assert.That(p_PR.ComputedValue, Is.EqualTo(0.31f).Within(Constants.TOLERANCE));
        Assert.That(PR.ModifiersEffect, Is.EqualTo(120).Within(Constants.TOLERANCE));

        Assert.That(p_MR.ComputedValue, Is.EqualTo(0.111f).Within(Constants.TOLERANCE));
        Assert.That(MR.ModifiersEffect, Is.EqualTo(0).Within(Constants.TOLERANCE));
    }
}
