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
        public override string Name => "无敌扇子";
        public override Character.Ability Type => Character.Ability.One;

        [SerializeField]
        private Fan _fanPrefab;

        [SerializeField]
        private Transform _fanThrowPoint;

        [SerializeField]
        private PassiveEffectSO _passiveEffectSO;

        [SerializeField]
        private AbilityOneSO _data;
        public override int ManaCost => CurrentLevel == 0 ? 0 : _data[CurrentLevel].ManaCost;

        private static readonly int ABILITY_ONE = Animator.StringToHash("Ability One");
        private Animator _animator;
        private Character _host;
        private EffectSystem _effectSystem;
        private PassiveEffect _passiveEffect;
        private Stat _md;

        public override void Awake()
        {
            base.Awake();
            _host = GetComponent<Character>();
            _animator = GetComponent<Animator>();
            _effectSystem = GetComponent<EffectSystem>();
            _passiveEffect = _passiveEffectSO.CreateEffect();
            MaxLevel = _data.Count;
            LevelUp();
        }

        void Start()
        {
            _md = _host.Statistics.GetStat(StatType.MagicalDamage);
        }

        private readonly AwaitableCompletionSource _abilityOneCompletionSource = new();

        protected override async Awaitable PerformAction()
        {
            _manaSystem.Consume(ManaCost);
            _abilityOneCompletionSource.Reset();
            // 小乔向指定方向扔出一把回旋飞行的扇子，
            // 会对第一个命中的敌人造成585/635/685/735/785/835（+80％法术加成）点法术伤害，
            // 每次命中后伤害都会衰减20％，最低衰减至初始伤害的50％。
            // Rotate towards the direction specified by the ab one input
            await _host.RotateTowards(
                new Vector3(_host.AbilityOneDirection.x, 0, _host.AbilityOneDirection.y)
            );

            // Throw a fan
            _animator.SetTrigger(ABILITY_ONE);
            await _abilityOneCompletionSource.Awaitable;
        }

        public void ThrowFan()
        {
            Fan fan = Instantiate(_fanPrefab, _fanThrowPoint.position, _fanThrowPoint.rotation);
            fan.SetReceiver(_host);
            fan.OnHit += (enemy, reductionRate) =>
            {
                float baseDamageAmount =
                    (_data[CurrentLevel].DamageAmount + (0.8f * _md.ComputedValue)) * reductionRate;
                var damage = new Damage(_host, enemy, DamageType.Magical, baseDamageAmount);
                enemy.Damageable.TakeDamage(damage);
                _effectSystem.AddEffect(_passiveEffect);
            };
            fan.Throw();
            _abilityOneCompletionSource.SetResult();
        }
    }
}
