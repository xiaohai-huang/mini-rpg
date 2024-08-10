using Core.Game.Combat;
using Core.Game.Statistics;
using NUnit.Framework;

public class DamageTests
{
    private static StatSystem CreateHanXinDoll()
    {
        var hx = new StatSystem();
        // MR=200 PR=400
        hx.AddModifier(new FloatModifier(StatType.MagicalResistance, 200f));
        hx.AddModifier(new FloatModifier(StatType.PhysicalResistance, 400f));

        return hx;
    }

    [Test]
    public void Test01()
    {
        var A = new StatSystem();
        var B = CreateHanXinDoll();

        A.AddModifier(new FloatModifier(StatType.PhysicalDamage, 600f));
        A.AddModifier(new FloatModifier(StatType.FlatPhysicalResistancePenetration, 50f));
        A.AddModifier(new FloatModifier(StatType.PercentagePhysicalResistancePenetration, 0.2f));

        Assert.That(
            (int)new Damage(A, B, DamageType.Physical, 600f).ComputedValue,
            Is.EqualTo(409)
        );
    }

    [Test]
    public void Test02()
    {
        // A: XiaoQiao
        // B: HanXin Doll
        var A = new StatSystem();
        var B = CreateHanXinDoll();

        // Stats
        var md = A.GetStat(StatType.MagicalDamage);

        // XiaoQiao Empty setup
        A.AddModifier(new FloatModifier(StatType.MagicalDamage, 0));
        A.AddModifier(new FloatModifier(StatType.FlatMagicalResistancePenetration, 0));
        A.AddModifier(new FloatModifier(StatType.PercentageMagicalResistancePenetration, 0));

        int getAbilityTwoDamage() =>
            (int)
                new Damage(A, B, DamageType.Magical, 300 + (0.5f * md.ComputedValue)).ComputedValue;

        // XiaoQiao ability 2 deals 300 magical damage
        Assert.That(getAbilityTwoDamage(), Is.EqualTo(225));

        // XiaoQiao equipped a book that can +40 Magical Damage
        A.AddModifier(new FloatModifier(StatType.MagicalDamage, 40));

        // XiaoQiao ability 2 deals 300 + (50% * Magical Damage) == 320 Magical Damage
        Assert.That(getAbilityTwoDamage(), Is.EqualTo(240));

        // XiaoQiao equipped a pair of secret shoes that can +100 Magical Penetration
        A.AddModifier(new FloatModifier(StatType.FlatMagicalResistancePenetration, 100));

        // XiaoQiao ability 2 deals 300 + (50% * Magical Damage) == 320 Magical Damage
        Assert.That(getAbilityTwoDamage(), Is.EqualTo(274));

        // XiaoQiao equipped 虚无法杖 +240 Magical Damage & +45% Magical Resistance Penetration
        A.AddModifier(new FloatModifier(StatType.MagicalDamage, 240));
        A.AddModifier(new FloatModifier(StatType.PercentageMagicalResistancePenetration, 0.45f));

        // XiaoQiao ability 2 deals 300 + (50% * Magical Damage)
        Assert.That(getAbilityTwoDamage(), Is.EqualTo(403));

        // XiaoQiao equipped 博学者之怒 +240 Magical Damage & +30% Magical Damage
        A.AddModifier(new FloatModifier(StatType.MagicalDamage, 240));
        A.AddModifier(new PercentageModifier(StatType.MagicalDamage, 0.3f));

        Assert.That((int)md.ModifiersEffect, Is.EqualTo(676));
        // XiaoQiao ability 2 deals 300 + (50% * Magical Damage)
        Assert.That(getAbilityTwoDamage(), Is.EqualTo(584));

        // XiaoQiao equipped 元素杖 +80 Magical Damage
        A.AddModifier(new FloatModifier(StatType.MagicalDamage, 80));
        Assert.That((int)md.ModifiersEffect, Is.EqualTo(780));

        // XiaoQiao ability 2 deals 300 + (50% * Magical Damage)
        Assert.That(getAbilityTwoDamage(), Is.EqualTo(632));
    }

    [Test]
    public void Test03()
    {
        // A: HouYi
        // B: HanXin Doll
        var A = new StatSystem();
        var B = CreateHanXinDoll();

        // lv.4 HouYi
        Stat pd = A.GetStat(StatType.PhysicalDamage);
        pd.BaseValue = 207;
        A.AddModifier(new FloatModifier(StatType.PhysicalDamage, 9));
        A.AddModifier(new FloatModifier(StatType.FlatPhysicalResistancePenetration, 64));
        int getAbilityThreeDamage() =>
            (int)
                new Damage(
                    A,
                    B,
                    DamageType.Physical,
                    700 + (0.9f * pd.ComputedValue)
                ).ComputedValue;

        // No items
        Assert.That((int)pd.ComputedValue, Is.EqualTo(216));
        // HouYi perform Ability 3
        Assert.That(getAbilityThreeDamage(), Is.EqualTo(573));
    }
}
