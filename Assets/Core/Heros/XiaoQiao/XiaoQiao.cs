using Unity.Mathematics;
using UnityEngine;

namespace Xiaohai.Character.XiaoQiao
{
    public class XiaoQiao : Character
    {
        [Header("XiaoQiao")]
        [SerializeField]
        private float _rotationSpeed;

        [Header("Ability One")]
        [SerializeField]
        private Fan _fanPrefab;

        [SerializeField]
        private Transform _fanThrowPoint;

        [Header("Ability Two")]
        [SerializeField]
        private GameObject _wind;

        [SerializeField]
        [Range(0.5f, 30f)]
        [Tooltip("Wind range radius")]
        private float _windMaxRange;

        [SerializeField]
        [Range(0.1f, 30f)]
        [Tooltip("Wind radius")]
        private float _windSize;

        private static readonly int ABILITY_ONE = Animator.StringToHash("Ability One");
        private Animator _animator;

        public override void Awake()
        {
            base.Awake();
            _animator = GetComponent<Animator>();
        }

        public override void Update()
        {
            base.Update();
        }

        private readonly AwaitableCompletionSource _abilityOneCompletionSource = new();

        public async Awaitable PerformAbilityOne()
        {
            _abilityOneCompletionSource.Reset();
            // 小乔向指定方向扔出一把回旋飞行的扇子，
            // 会对第一个命中的敌人造成585/635/685/735/785/835（+80％法术加成）点法术伤害，
            // 每次命中后伤害都会衰减20％，最低衰减至初始伤害的50％。
            Debug.Log("Start to perform XQ Ab 1");
            // Rotate towards the direction specified by the ab one input
            await RotateTowards(new Vector3(AbilityOneDirection.x, 0, AbilityOneDirection.y));
            // Throw a fan
            _animator.SetTrigger(ABILITY_ONE);
            await _abilityOneCompletionSource.Awaitable;
        }

        public void ThrowFan()
        {
            Fan fan = Instantiate(_fanPrefab, _fanThrowPoint.position, _fanThrowPoint.rotation);
            fan.SetReceiver(transform);
            fan.Throw(585);
            _abilityOneCompletionSource.SetResult();
        }

        private async Awaitable RotateTowards(Vector3 targetDirection)
        {
            // Rotate towards the direction
            if (targetDirection != Vector3.zero)
            {
                var targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
                float angles = Vector3.Angle(transform.forward, targetDirection);
                while (angles > 1f)
                {
                    transform.rotation = Quaternion.Slerp(
                        transform.rotation,
                        targetRotation,
                        Time.deltaTime * _rotationSpeed
                    );

                    angles = Vector3.Angle(transform.forward, targetDirection);
                    await Awaitable.NextFrameAsync();
                }
            }
        }

        public async Awaitable PerformAbilityTwo()
        {
            // 小乔在指定区域召唤出一道旋风，
            // 对区域内敌人造成300/340/380/420/460/500（+50％法术加成）点法术伤害并击飞1.5秒，攻击盒半径240
            Debug.Log("Start to perform XQ Ab 2");
            // Get the world position of the attack area
            var offset = _windMaxRange * AbilityTwoPosition;
            var attackPosition = transform.position + new Vector3(offset.x, 0, offset.y);

            await Awaitable.WaitForSecondsAsync(0.1f);
            var wind = Instantiate(_wind, attackPosition, Quaternion.identity);
            wind.transform.localScale = new Vector3(_windSize * 2, 1, _windSize * 2);
        }

        public void PerformAbilityThree()
        {
            // 小乔召唤流星并不断向附近的敌人坠落，召唤持续6秒，
            // 每颗流星会造成400/500/600（+100％法术加成）点法术伤害，
            // 每个敌人最多承受4次攻击，
            // 当多颗流星命中同一目标时，从第二颗流星开始将只造成50％伤害。释放期间持续获得被动加速效果。
            Debug.Log("Start to perform XQ Ab 3");
        }
    }
}
