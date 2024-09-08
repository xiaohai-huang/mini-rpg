using System;
using UnityEngine;

namespace Core.Game.Effects
{
    [CreateAssetMenu(
        fileName = "Gain XP Effect",
        menuName = "My Scriptable Objects/Effects/Gain XP"
    )]
    public class GainXPEffectSO : EffectSO<GainXPEffect>
    {
        public int XP_To_Gain;

        /// <summary>
        /// Interval in seconds
        /// </summary>
        public float Interval;
    }

    public class GainXPEffect : Effect
    {
        public new GainXPEffectSO OriginSO => (GainXPEffectSO)base.OriginSO;

        public override Action OnApply(EffectSystem system)
        {
            base.OnApply(system);
            // gain x xp per N seconds
            int timer = Timer.Instance.SetInterval(
                () =>
                {
                    system.Host.Level.IncreaseXP(OriginSO.XP_To_Gain);
                },
                OriginSO.Interval * 1000
            );

            return () =>
            {
                Timer.Instance.ClearInterval(timer);
            };
        }
    }
}
