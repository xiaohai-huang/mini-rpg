using Core.Game.Statistics;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

namespace Xiaohai.Turret.Actions
{
    [CreateAssetMenu(
        fileName = "AttackAction",
        menuName = "State Machines/Actions/Turret/Attack Action"
    )]
    public class AttackActionSO : StateActionSO
    {
        protected override StateAction CreateAction() => new AttackAction();
    }

    public class AttackAction : StateAction
    {
        protected new AttackActionSO OriginSO => (AttackActionSO)base.OriginSO;
        private Turret _turret;

        public override void Awake(StateMachine stateMachine)
        {
            _turret = stateMachine.GetComponent<Turret>();
        }

        public override void OnUpdate()
        {
            if (_turret.Target != null)
            {
                _turret.AttackBallTarget.position = Vector3.Lerp(
                    _turret.AttackBallTarget.position,
                    _turret.Target.transform.position,
                    5f * Time.deltaTime
                );
            }
        }

        private int _attackTimer;
        private int _startAttackTimer;

        public override void OnStateEnter()
        {
            // Delay for 500ms and then start the attack
            _startAttackTimer = Timer.Instance.SetTimeout(
                () =>
                {
                    _attackTimer = Timer.Instance.SetInterval(
                        () =>
                        {
                            if (_turret.Target == null)
                                return;

                            var projectile = Object.Instantiate(_turret.BulletPrefab);
                            projectile.SetTarget(_turret.Target);
                            projectile.transform.position = _turret.FirePoint.position;
                            projectile.Speed = 10f;
                            projectile.DamageAmount = (int)
                                _turret.Statistics.GetStat(StatType.PhysicalDamage).ComputedValue;
                        },
                        1000f / _turret.AttackSpeed,
                        true
                    );
                },
                500f
            );
        }

        public override void OnStateExit()
        {
            Timer.Instance.ClearTimeout(_startAttackTimer);
            Timer.Instance.ClearInterval(_attackTimer);
        }
    }
}
