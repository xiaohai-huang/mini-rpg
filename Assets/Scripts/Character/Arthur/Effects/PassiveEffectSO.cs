using Core.Game.Entities;
using Core.Game.Statistics;
using UnityEngine;

namespace Xiaohai.Character.Arthur
{
    [CreateAssetMenu(
        fileName = "Arthur Passive Effect",
        menuName = "My Scriptable Objects/Effects/Character/Arthur/Passive Effect"
    )]
    public class PassiveEffectSO : EffectSO<PassiveEffect>
    {
        /// <summary>
        /// The percentage of the Maximum Health to recover.
        /// </summary>
        public float RecoverPercentage;

        /// <summary>
        /// The frequency of the recover in ms. 2000ms means performing a recover every 2 seconds.
        /// </summary>
        public float RecoverRate;
    }

    /// <summary>
    /// Recover 2% of his Maximum Health every 2 seconds.
    /// </summary>
    public class PassiveEffect : Effect
    {
        public new PassiveEffectSO OriginSO => (PassiveEffectSO)base.OriginSO;
        private int _timer;

        public override void OnApply(EffectSystem system)
        {
            base.OnApply(system);
            var maxHealth = system.GetComponent<Base>().Statistics.GetStat(StatType.MaxHealth);
            _timer = Timer.Instance.SetInterval(
                () =>
                {
                    system.RestoreHealth(
                        (int)(maxHealth.ComputedValue * OriginSO.RecoverPercentage)
                    );
                },
                OriginSO.RecoverRate
            );
        }

        public override void OnRemove(EffectSystem system)
        {
            base.OnRemove(system);
            Timer.Instance.ClearInterval(_timer);
        }
    }
}
