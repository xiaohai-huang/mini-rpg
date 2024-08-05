using System.Collections;
using Core.Game.Statistics;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

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
}
