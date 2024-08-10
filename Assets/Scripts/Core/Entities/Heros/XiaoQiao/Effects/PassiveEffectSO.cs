using System;
using Core.Game.Entities;
using Core.Game.Statistics;
using UnityEngine;

namespace Core.Game.Heros.XiaoQiao
{
    [CreateAssetMenu(
        fileName = "XiaoQiao Passive Effect",
        menuName = "My Scriptable Objects/Effects/Character/XiaoQiao/Passive Effect"
    )]
    public class PassiveEffectSO : EffectSO<PassiveEffect>
    {
        public float SpeedUpPercentage;
    }

    public class PassiveEffect : Effect
    {
        public new PassiveEffectSO OriginSO => (PassiveEffectSO)base.OriginSO;

        public override Action OnApply(EffectSystem system)
        {
            base.OnApply(system);
            var statSystem = system.GetComponent<Base>().Statistics;
            var speedUpModifier = new PercentageModifier(
                StatType.MovementSpeed,
                OriginSO.SpeedUpPercentage
            );
            statSystem.AddModifier(speedUpModifier);

            int timerId = Timer.Instance.SetTimeout(
                () =>
                {
                    Finished = true;
                },
                OriginSO.CoolDownTime
            );
            return () =>
            {
                statSystem.RemoveModifier(speedUpModifier);
                Timer.Instance.ClearTimeout(timerId);
            };
        }
    }
}
