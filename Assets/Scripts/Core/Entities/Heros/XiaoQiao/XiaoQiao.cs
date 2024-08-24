using Core.Game.Combat;
using Core.Game.Effects;
using Core.Game.Heros.XiaoQiao;
using Core.Game.Mana;
using Core.Game.Statistics;
using UnityEngine;

namespace Xiaohai.Character.XiaoQiao
{
    public class XiaoQiao : Character
    {
        [Header("XiaoQiao")]
        [Header("Ability Three")]
        [SerializeField]
        private MeteorRain _meteorRain;

        [SerializeField]
        [Range(0.1f, 30f)]
        [Tooltip("Meteor rain radius")]
        private float _meteorRainRange;

        [Header("Passive")]
        [SerializeField]
        private PassiveEffectSO _passiveEffectSO;

        /// <summary>
        /// Magical Damage
        /// </summary>
        private Stat _md;

        private EffectSystem _effectSystem;
        private PassiveEffect _passiveEffect;
        private ManaSystem _manaSystem;

        public override void Awake()
        {
            base.Awake();
            _effectSystem = GetComponent<EffectSystem>();
            _passiveEffect = _passiveEffectSO.CreateEffect();
            _manaSystem = GetComponent<ManaSystem>();
        }

        void Start()
        {
            _md = Statistics.GetStat(StatType.MagicalDamage);
            // Add Regenerate effect
            _effectSystem.AddEffect(new RegenerateEffect());
        }

        public override void Update()
        {
            base.Update();
        }

        // final 225
        // target: MR=200
        // damageReductionRate = 200 / (200 + 602)
        public void PerformAbilityThree()
        {
            // 小乔召唤流星并不断向附近的敌人坠落，召唤持续6秒，
            // 每颗流星会造成400/500/600（+100％法术加成）点法术伤害，
            // 每个敌人最多承受4次攻击，
            // 当多颗流星命中同一目标时，从第二颗流星开始将只造成50％伤害。
            // 释放期间持续获得被动加速效果。
            _manaSystem.Consume(140);

            float ONE_SECOND = 1000f;
            int timerId = Timer.Instance.SetInterval(
                () =>
                {
                    _effectSystem.AddEffect(_passiveEffect);
                },
                ONE_SECOND,
                true
            );

            var meteorRain = Instantiate(_meteorRain, transform);
            meteorRain.transform.position += Vector3.up * 0.04f;
            meteorRain.OnHit += (enemy, reductionRate) =>
            {
                float baseDamageAmount = (400 + _md.ComputedValue) * reductionRate;
                var damage = new Damage(this, enemy, DamageType.Magical, baseDamageAmount);
                enemy.Damageable.TakeDamage(damage);
                _effectSystem.AddEffect(_passiveEffect);
            };
            meteorRain.OnFinished += () =>
            {
                Timer.Instance.ClearInterval(timerId);
            };
        }
    }
}
