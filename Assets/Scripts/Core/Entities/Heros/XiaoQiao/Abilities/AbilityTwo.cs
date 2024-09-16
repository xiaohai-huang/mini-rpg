using Core.Game.Combat;
using Core.Game.Heros.XiaoQiao;
using Core.Game.Statistics;
using UnityEngine;
using Xiaohai.Character.XiaoQiao;

namespace Core.Game.Entities.Heros.XiaoQiao
{
    public class AbilityTwo : AbilityTwoBase
    {
        public override string Name => "甜蜜恋风";

        public override int ManaCost => CurrentLevel == 0 ? 0 : _data[CurrentLevel].ManaCost;
        public override float Total_CD_Timer => _data[CurrentLevel].CoolDown;
        public override bool Upgradable =>
            NextLevel <= MaxLevel
            && Host.Level.Value >= _data[NextLevel].UnlockAtPlayerLevel
            && Host.Level.AbilityUpgradeCredits > 0;

        [SerializeField]
        private Wind _wind;

        [SerializeField]
        [Range(0.5f, 30f)]
        [Tooltip("Wind range radius")]
        private float _windMaxRange;

        [SerializeField]
        [Range(0.1f, 30f)]
        [Tooltip("Wind radius")]
        private float _windSize;

        [SerializeField]
        private PassiveEffectSO _passiveEffectSO;

        [SerializeField]
        private AbilityTwoInfoSO _data;

        private readonly float _flyingDuration = 1.5f;

        private EffectSystem _effectSystem;
        private Effect _passiveEffect;
        private Stat _md;

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
            // 小乔在指定区域召唤出一道旋风，
            // 对区域内敌人造成300/340/380/420/460/500（+50％法术加成）点法术伤害并击飞1.5秒，攻击盒半径240
            CD_Timer = Total_CD_Timer;
            ManaSystem.Consume(ManaCost);

            // Get the world position of the attack area
            var offset = _windMaxRange * Host.AbilityTwoPosition;
            var attackPosition = transform.position + new Vector3(offset.x, 0, offset.y);

            await Awaitable.WaitForSecondsAsync(0.1f);
            Wind wind = Instantiate(_wind, attackPosition, Quaternion.identity);
            wind.transform.localScale = new Vector3(_windSize * 2, 1, _windSize * 2);
            float baseDamageAmount = _data[CurrentLevel].DamageAmount + (_md.ComputedValue * 0.5f);
            wind.OnHit += (enemy) =>
            {
                var damage = new Damage(Host, enemy, DamageType.Magical, baseDamageAmount);
                enemy.Damageable.TakeDamage(damage);

                // Launch the target into sky for a given time
                enemy.KnockUp(_flyingDuration);
                _effectSystem.AddEffect(_passiveEffect);
            };
        }
    }
}
