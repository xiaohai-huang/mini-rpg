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
       

        [Header("Ability One")]
        [SerializeField]
        private Fan _fanPrefab;

        [SerializeField]
        private Transform _fanThrowPoint;

        [Header("Ability Two")]
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

        private float _flyingDuration = 1.5f;

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

        private static readonly int ABILITY_ONE = Animator.StringToHash("Ability One");
        private Animator _animator;

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
            _animator = GetComponent<Animator>();
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

        private readonly AwaitableCompletionSource _abilityOneCompletionSource = new();

        public async Awaitable PerformAbilityOne()
        {
            _manaSystem.Consume(45);
            _abilityOneCompletionSource.Reset();
            // 小乔向指定方向扔出一把回旋飞行的扇子，
            // 会对第一个命中的敌人造成585/635/685/735/785/835（+80％法术加成）点法术伤害，
            // 每次命中后伤害都会衰减20％，最低衰减至初始伤害的50％。
            // Rotate towards the direction specified by the ab one input
            await RotateTowards(new Vector3(AbilityOneDirection.x, 0, AbilityOneDirection.y));
            // Throw a fan
            _animator.SetTrigger(ABILITY_ONE);
            await _abilityOneCompletionSource.Awaitable;
        }

        public void ThrowFan()
        {
            Fan fan = Instantiate(_fanPrefab, _fanThrowPoint.position, _fanThrowPoint.rotation);
            fan.SetReceiver(this);
            fan.OnHit += (enemy, reductionRate) =>
            {
                float baseDamageAmount = (585f + (0.8f * _md.ComputedValue)) * reductionRate;
                var damage = new Damage(this, enemy, DamageType.Magical, baseDamageAmount);
                enemy.Damageable.TakeDamage(damage);
                _effectSystem.AddEffect(_passiveEffect);
            };
            fan.Throw();
            _abilityOneCompletionSource.SetResult();
        }

        public async Awaitable PerformAbilityTwo()
        {
            // 小乔在指定区域召唤出一道旋风，
            // 对区域内敌人造成300/340/380/420/460/500（+50％法术加成）点法术伤害并击飞1.5秒，攻击盒半径240
            _manaSystem.Consume(70);

            // Get the world position of the attack area
            var offset = _windMaxRange * AbilityTwoPosition;
            var attackPosition = transform.position + new Vector3(offset.x, 0, offset.y);

            await Awaitable.WaitForSecondsAsync(0.1f);
            Wind wind = Instantiate(_wind, attackPosition, Quaternion.identity);
            wind.transform.localScale = new Vector3(_windSize * 2, 1, _windSize * 2);
            wind.OnHit += (enemy) =>
            {
                float baseDamageAmount = 300f + (_md.ComputedValue * 0.5f);
                var damage = new Damage(this, enemy, DamageType.Magical, baseDamageAmount);
                enemy.Damageable.TakeDamage(damage);

                // Launch the target into sky for a given time
                enemy.KnockUp(_flyingDuration);
                _effectSystem.AddEffect(_passiveEffect);
            };
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
