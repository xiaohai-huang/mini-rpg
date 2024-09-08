using System;
using UnityEngine;

namespace Core.Game.Effects
{
    [CreateAssetMenu(
        fileName = "Regenerate Effect",
        menuName = "My Scriptable Objects/Effects/Regenerate"
    )]
    public class RegenerateEffectSO : EffectSO<RegenerateEffect> { }

    public class RegenerateEffect : Effect
    {
        public override Action OnApply(EffectSystem system)
        {
            base.OnApply(system);
            // regenerate health, mana
            float manaRecoveredPerSecond = system
                .Host.Statistics.GetStat(Statistics.StatType.ManaRecoveredPerSecond)
                .ComputedValue;
            float healthRecoveredPerSecond = system
                .Host.Statistics.GetStat(Statistics.StatType.HealthRecoveredPerSecond)
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
