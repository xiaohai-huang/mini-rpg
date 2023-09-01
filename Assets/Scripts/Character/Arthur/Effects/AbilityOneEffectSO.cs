using UnityEngine;

namespace Xiaohai.Character.Arthur
{

    [CreateAssetMenu(fileName = "Arthur Ability One Speed Up Effect", menuName = "My Scriptable Objects/Effects/Character/Arthur/ Ability One Speed Up Effect")]
    public class AbilityOneEffectSO : EffectSO<AbilityOneEffect>
    {
    }

    /// <summary>
    /// 增加30%移动速度，持续3秒
    /// </summary>
    public class AbilityOneEffect : Effect
    {
        private float _speedPercentage;
        private Character _character;
        private int _timerId;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="speedUpPercentage"></param>
        /// <param name="duration">Duration of the effect in milliseconds.</param>
        public void Init(float speedUpPercentage, float duration)
        {
            _speedPercentage = speedUpPercentage;
            CoolDownTime = duration;
        }

        public override void OnApply(EffectSystem system)
        {
            base.OnApply(system);
            _character = system.GetComponent<Character>();
            _character.BonusWalkSpeed += _speedPercentage * _character.BaseWalkSpeed;
            _timerId = Timer.Instance.SetTimeout(() => Finished = true, CoolDownTime);
        }

        public override void OnRemove(EffectSystem system)
        {
            base.OnRemove(system);
            _character.BonusWalkSpeed -= _speedPercentage * _character.BaseWalkSpeed;
            Timer.Instance.ClearTimeout(_timerId);
        }
    }
}