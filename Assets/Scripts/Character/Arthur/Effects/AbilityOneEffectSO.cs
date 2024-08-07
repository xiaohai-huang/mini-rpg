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
        private int _timerId;
        private GameObject _indicator;

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

        public override void OnApply(EffectSystem system)
        {
            base.OnApply(system);
            _character = system.GetComponent<Character>();
            _character.BonusWalkSpeed += _speedPercentage * _character.BaseWalkSpeed;

            // show the visual of enhanced basic attack range indicator
            _indicator = Object.Instantiate(OriginSO.RangeIndicatorPrefab, system.transform);
            _indicator.transform.localScale = new Vector3(
                _enhancedBasicAttackRange * 2,
                _enhancedBasicAttackRange * 2,
                1f
            );
            _indicator.transform.localPosition = Vector3.zero;
            _indicator.transform.localPosition += OriginSO.Offset;
            _indicator.SetActive(true);
            _timerId = Timer.Instance.SetTimeout(() => Finished = true, CoolDownTime);
        }

        public override void OnRemove(EffectSystem system)
        {
            base.OnRemove(system);
            Object.Destroy(_indicator);
            _character.BonusWalkSpeed -= _speedPercentage * _character.BaseWalkSpeed;
            Timer.Instance.ClearTimeout(_timerId);
        }
    }
}
