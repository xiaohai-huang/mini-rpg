using UnityEngine;

namespace Xiaohai.Character.XiaoQiao
{
    public class XiaoQiao : Character
    {
        [Header("XiaoQiao")]

        [Header("Ability One")]
        [SerializeField] private Fan _fanPrefab;
        [SerializeField] private Transform _fanThrowPoint;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        public override void Update()
        {
            base.Update();
        }

        public void PerformAbilityOne()
        {
            // 小乔向指定方向扔出一把回旋飞行的扇子，
            // 会对第一个命中的敌人造成585/635/685/735/785/835（+80％法术加成）点法术伤害，
            // 每次命中后伤害都会衰减20％，最低衰减至初始伤害的50％。
            Debug.Log("Start to perform XQ Ab 1");

            // Throw a fan
            // Adjust the throw point based on ability one input direction
            _fanThrowPoint.rotation = Quaternion.LookRotation(new Vector3(AbilityOneDirection.x, 0, AbilityOneDirection.y), Vector3.up);

            Fan fan = Instantiate(_fanPrefab, _fanThrowPoint.position, _fanThrowPoint.rotation);
            fan.SetReceiver(transform);
            fan.Throw();
        }

        public void PerformAbilityTwo()
        {
            // 小乔在指定区域召唤出一道旋风，
            // 对区域内敌人造成300/340/380/420/460/500（+50％法术加成）点法术伤害并击飞1.5秒，攻击盒半径240
            Debug.Log("Start to perform XQ Ab 2");
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
