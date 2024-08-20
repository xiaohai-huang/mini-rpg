using System;

namespace Core.Game.Effects
{
    public class RegenerateEffect : Effect
    {
        public RegenerateEffect()
        {
            Name = "Regenerate Effect";
        }

        public override Action OnApply(EffectSystem system)
        {
            base.OnApply(system);
            // regenerate health, mana
            float manaRecoveredPerSecond = system
                .Entity.Statistics.GetStat(Statistics.StatType.ManaRecoveredPerSecond)
                .ComputedValue;
            float healthRecoveredPerSecond = system
                .Entity.Statistics.GetStat(Statistics.StatType.HealthRecoveredPerSecond)
                .ComputedValue;
            int timerId = Timer.Instance.SetInterval(
                () =>
                {
                    system.RecoverMana((int)manaRecoveredPerSecond);
                    system.RestoreHealth((int)healthRecoveredPerSecond);
                },
                1000f
            );

            return () =>
            {
                Timer.Instance.ClearInterval(timerId);
            };
        }
    }
}
