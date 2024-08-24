using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

namespace Core.Game.Entities.Heros
{
    [CreateAssetMenu(
        fileName = "Apply Effects Action",
        menuName = "State Machines/Actions/Heros/Apply Effects Action"
    )]
    public class ApplyEffectsActionSO : StateActionSO
    {
        protected override StateAction CreateAction() => new ApplyEffectsAction();

        public EffectSO[] Effects;
    }

    public class ApplyEffectsAction : StateAction
    {
        protected new ApplyEffectsActionSO OriginSO => (ApplyEffectsActionSO)base.OriginSO;
        private EffectSystem _system;

        public override void Awake(StateMachine stateMachine)
        {
            _system = stateMachine.GetComponent<EffectSystem>();
        }

        public override void OnUpdate() { }

        public override void OnStateEnter()
        {
            foreach (var effectSO in OriginSO.Effects)
            {
                _system.AddEffect(effectSO.CreateEffect());
            }
        }
    }
}
