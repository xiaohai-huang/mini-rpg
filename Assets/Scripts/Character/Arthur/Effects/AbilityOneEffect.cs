using UnityEngine;

namespace Xiaohai.Character.Arthur
{
    /// <summary>
    /// 增加30%移动速度，持续3秒
    /// </summary>
    public class AbilityOneEffect : Effect
    {
        private readonly float _speedPercentage;
        private readonly float _duration;
        private Character _character;
        private int _timerId;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="speedUpPercentage"></param>
        /// <param name="duration">Duration of the effect in milliseconds.</param>
        public AbilityOneEffect(float speedUpPercentage, float duration)
        {
            Name = "Arthur Ability One Speed Up";
            _speedPercentage = speedUpPercentage;
            _duration = duration;
        }

        public override void OnApply(EffectSystem system)
        {
            base.OnApply(system);
            _character = system.GetComponent<Character>();
            _character.BonusWalkSpeed += _speedPercentage * _character.BaseWalkSpeed;
            _timerId = Timer.Instance.SetTimeout(() => Finished = true, _duration);
        }

        public override void OnRemove(EffectSystem system)
        {
            _character.BonusWalkSpeed -= _speedPercentage * _character.BaseWalkSpeed;
            Timer.Instance.ClearTimeout(_timerId);
        }
    }
}