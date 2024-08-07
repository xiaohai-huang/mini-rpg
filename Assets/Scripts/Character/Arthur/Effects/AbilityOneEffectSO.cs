using System;
using Core.Game.Statistics;
using UnityEngine;

namespace Xiaohai.Character.Arthur
{
    [CreateAssetMenu(
        fileName = "Arthur Ability One Speed Up Effect",
        menuName = "My Scriptable Objects/Effects/Character/Arthur/Ability One Speed Up Effect"
    )]
    public class AbilityOneEffectSO : EffectSO<AbilityOneEffect>
    {
        public GameObject RangeIndicatorPrefab;
        public Vector3 Offset;
    }

    /// <summary>
    /// 增加30%移动速度，持续3秒
    /// </summary>
    public class AbilityOneEffect : Effect
    {
        public new AbilityOneEffectSO OriginSO => (AbilityOneEffectSO)base.OriginSO;
        private float _speedPercentage;
        private float _enhancedBasicAttackRange;
        private Character _character;

        /// <summary>
        ///
        /// </summary>
        /// <param name="speedUpPercentage"></param>
        /// <param name="duration">Duration of the effect in milliseconds.</param>
        public void Init(float speedUpPercentage, float duration, float enhancedBasicAttackRange)
        {
            _speedPercentage = speedUpPercentage;
            _enhancedBasicAttackRange = enhancedBasicAttackRange;
            CoolDownTime = duration;
        }

        public override Action OnApply(EffectSystem system)
        {
            base.OnApply(system);
            _character = system.GetComponent<Character>();
            var speedUpModifier = new PercentageModifier(StatType.MovementSpeed, _speedPercentage);
            _character.Statistics.AddModifier(speedUpModifier);

            // show the visual of enhanced basic attack range indicator
            GameObject indicator = UnityEngine.Object.Instantiate(
                OriginSO.RangeIndicatorPrefab,
                system.transform
            );
            indicator.transform.localScale = new Vector3(
                _enhancedBasicAttackRange * 2,
                _enhancedBasicAttackRange * 2,
                1f
            );
            indicator.transform.localPosition = Vector3.zero;
            indicator.transform.localPosition += OriginSO.Offset;
            indicator.SetActive(true);
            int timerId = Timer.Instance.SetTimeout(() => Finished = true, CoolDownTime);

            return () =>
            {
                _character.Statistics.RemoveModifier(speedUpModifier);
                UnityEngine.Object.Destroy(indicator);
                Timer.Instance.ClearTimeout(timerId);
            };
        }
    }
}
