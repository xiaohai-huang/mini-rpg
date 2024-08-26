using Core.Game.Combat;
using Core.Game.Heros.XiaoQiao;
using Core.Game.Statistics;
using UnityEngine;
using Xiaohai.Character;
using Xiaohai.Character.XiaoQiao;

namespace Core.Game.Entities.Heros.XiaoQiao
{
    public class AbilityOne : AbilityBase
    {
        public override string Name => "绽放之舞";
        public override Character.Ability Type => Character.Ability.One;

        [SerializeField]
        private Fan _fanPrefab;

        [SerializeField]
        private Transform _fanThrowPoint;

        [SerializeField]
        private PassiveEffectSO _passiveEffectSO;

        [SerializeField]
        private AbilityOneInfoSO _data;
        public override int ManaCost => CurrentLevel == 0 ? 0 : _data[CurrentLevel].ManaCost;

        private static readonly int ABILITY_ONE = Animator.StringToHash("Ability One");
        private Animator _animator;
        private EffectSystem _effectSystem;
        private Effect _passiveEffect;
        private Stat _md;

        public override void Awake()
        {
            base.Awake();
            _animator = GetComponent<Animator>();
            _effectSystem = GetComponent<EffectSystem>();
            _passiveEffect = _passiveEffectSO.CreateEffect();
        }

        void Start()
        {
            _md = Host.Statistics.GetStat(StatType.MagicalDamage);
            MaxLevel = _data.Count;
            LevelUp();
        }

        private readonly AwaitableCompletionSource _abilityOneCompletionSource = new();

        protected override async Awaitable PerformAction()
        {
            ManaSystem.Consume(ManaCost);
            _abilityOneCompletionSource.Reset();
            // 小乔向指定方向扔出一把回旋飞行的扇子，
            // 会对第一个命中的敌人造成585/635/685/735/785/835（+80％法术加成）点法术伤害，
            // 每次命中后伤害都会衰减20％，最低衰减至初始伤害的50％。
            // Rotate towards the direction specified by the ab one input
            await Host.RotateTowards(
                new Vector3(Host.AbilityOneDirection.x, 0, Host.AbilityOneDirection.y)
            );

            // Throw a fan
            _animator.SetTrigger(ABILITY_ONE);
            await _abilityOneCompletionSource.Awaitable;
        }

        public void ThrowFan()
        {
            Fan fan = Instantiate(_fanPrefab, _fanThrowPoint.position, _fanThrowPoint.rotation);
            fan.SetReceiver(Host);
            fan.OnHit += (enemy, reductionRate) =>
            {
                float baseDamageAmount =
                    (_data[CurrentLevel].DamageAmount + (0.8f * _md.ComputedValue)) * reductionRate;
                var damage = new Damage(Host, enemy, DamageType.Magical, baseDamageAmount);
                enemy.Damageable.TakeDamage(damage);
                _effectSystem.AddEffect(_passiveEffect);
            };
            fan.Throw();
            _abilityOneCompletionSource.SetResult();
        }
    }
}
