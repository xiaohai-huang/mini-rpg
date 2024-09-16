using System;
using Core.Game.Combat;
using Core.Game.Heros.XiaoQiao;
using Core.Game.Statistics;
using UnityEngine;
using Xiaohai.Character.XiaoQiao;

namespace Core.Game.Entities.Heros.XiaoQiao
{
    public class AbilityThree : AbilityThreeBase
    {
        public override string Name => "星华缭乱";

        public override int ManaCost => CurrentLevel == 0 ? 0 : _data[CurrentLevel].ManaCost;
        public override float Total_CD_Timer => _data[CurrentLevel].CoolDown;

        public override bool Upgradable =>
            NextLevel <= MaxLevel
            && Host.Level.Value >= _data[NextLevel].UnlockAtPlayerLevel
            && Host.Level.AbilityUpgradeCredits > 0;

        [SerializeField]
        private MeteorRain _meteorRain;

        [SerializeField]
        [Range(0.1f, 30f)]
        [Tooltip("Meteor rain radius")]
        private float _meteorRainRange;

        [SerializeField]
        private PassiveEffectSO _passiveEffectSO;

        [SerializeField]
        private AbilityThreeInfoSO _data;

        private Stat _md;

        private EffectSystem _effectSystem;
        private Effect _passiveEffect;

        public override void Awake()
        {
            base.Awake();
            _effectSystem = GetComponent<EffectSystem>();
            _passiveEffect = _passiveEffectSO.CreateEffect();
            MaxLevel = _data.Count;
        }

        void Start()
        {
            _md = Host.Statistics.GetStat(StatType.MagicalDamage);
        }

        protected override async Awaitable PerformAction()
        {
            // 小乔召唤流星并不断向附近的敌人坠落，召唤持续6秒，
            // 每颗流星会造成400/500/600（+100％法术加成）点法术伤害，
            // 每个敌人最多承受4次攻击，
            // 当多颗流星命中同一目标时，从第二颗流星开始将只造成50％伤害。
            // 释放期间持续获得被动加速效果。
            CD_Timer = Total_CD_Timer;
            ManaSystem.Consume(ManaCost);

            await Awaitable.WaitForSecondsAsync(0.1f);

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
            float baseDamageAmount = _data[CurrentLevel].DamageAmount + _md.ComputedValue;
            meteorRain.OnHit += (enemy, reductionRate) =>
            {
                var damage = new Damage(
                    Host,
                    enemy,
                    DamageType.Magical,
                    baseDamageAmount * reductionRate
                );
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
