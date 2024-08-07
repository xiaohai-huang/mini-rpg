using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using Xiaohai.Character;
using Xiaohai.Utilities;

namespace Core.Game.Entities.Turrets
{
    [CreateAssetMenu(
        fileName = "IdentifyTargetAction",
        menuName = "State Machines/Actions/Turret/Identify Target Action"
    )]
    public class IdentifyTargetActionSO : StateActionSO
    {
        protected override StateAction CreateAction() => new IdentifyTargetAction();
    }

    public class IdentifyTargetAction : StateAction
    {
        protected new IdentifyTargetActionSO OriginSO => (IdentifyTargetActionSO)base.OriginSO;
        private Turret _turret;

        public override void Awake(StateMachine stateMachine)
        {
            _turret = stateMachine.GetComponent<Turret>();
        }

        public override void OnUpdate()
        {
            _turret.Target = PickTarget(_turret.TargetSelection.Targets);
        }

        public override void OnStateEnter() { }

        public override void OnStateExit() { }

        Damageable PickTarget(List<TargetInfo<Damageable>> targets)
        {
            if (targets.Count == 0)
                return null;
            // Pick the first one that enters the area
            // The target should be alive
            Damageable target = null;

            for (int i = 0; i < _turret.TargetSelection.Targets.Count; i++)
            {
                if (!_turret.TargetSelection.Targets[i].target.IsDead)
                {
                    target = _turret.TargetSelection.Targets[i].target;
                    break;
                }
            }
            return target;
        }
    }
}
