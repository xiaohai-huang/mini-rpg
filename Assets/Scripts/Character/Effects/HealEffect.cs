using System;

public class HealEffect : Effect
{
    private readonly int _healAmount;

    public HealEffect(string name, int healAmount)
    {
        Name = name;
        _healAmount = healAmount;
    }

    public override Action OnApply(EffectSystem system)
    {
        system.RestoreHealth(_healAmount);
        Finished = true;
        return null;
    }
}
