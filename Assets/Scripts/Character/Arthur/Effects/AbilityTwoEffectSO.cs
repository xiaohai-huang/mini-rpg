using System;
using UnityEngine;

namespace Xiaohai.Character.Arthur
{
    [CreateAssetMenu(
        fileName = "Arthur Ability Two Shields Effect",
        menuName = "My Scriptable Objects/Effects/Character/Arthur/Ability Two Shields Effect"
    )]
    public class AbilityTwoEffectSO : EffectSO<AbilityTwoEffect>
    {
        public new AbilityTwoEffect CreateEffect() => (AbilityTwoEffect)base.CreateEffect();
    }

    /// <summary>
    /// Summons 3 shields that circle around him. Shields last for x seconds.
    /// </summary>
    public class AbilityTwoEffect : Effect
    {
        private float _damageAmount;

        private Shields _shields;
        private int _timerId;

        /// <summary>
        ///
        /// </summary>
        /// <param name="damageAmount"></param>
        /// <param name="duration">Duration of the effect in milliseconds.</param>
        public void Init(float damageAmount, float duration, Shields shields)
        {
            _damageAmount = damageAmount;
            CoolDownTime = duration;
            _shields = shields;
        }

        public override Action OnApply(EffectSystem system)
        {
            base.OnApply(system);
            _shields.StartSpin(_damageAmount);
            _timerId = Timer.Instance.SetTimeout(() => Finished = true, CoolDownTime);
            return null;
        }

        public override void OnRemove(EffectSystem system)
        {
            base.OnRemove(system);
            _shields.StopSpin();
            Timer.Instance.ClearTimeout(_timerId);
        }
    }
}
