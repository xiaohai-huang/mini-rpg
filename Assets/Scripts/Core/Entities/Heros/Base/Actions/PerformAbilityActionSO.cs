using Core.Game.Combat;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using Xiaohai.Character;

namespace Core.Game.Entities.Heros
{
    [CreateAssetMenu(
        fileName = "Perform Ability Action",
        menuName = "State Machines/Actions/Heros/Perform Ability Action"
    )]
    public class PerformAbilityActionSO : StateActionSO
    {
        protected override StateAction CreateAction() => new PerformAbilityAction();

        public Character.Ability Type;
    }

    public class PerformAbilityAction : StateAction
    {
        protected new PerformAbilityActionSO OriginSO => (PerformAbilityActionSO)base.OriginSO;

        private Character _character;
        private AbilityBase _ability;

        public override void Awake(StateMachine stateMachine)
        {
            _character = stateMachine.GetComponent<Character>();
            var abilities = stateMachine.GetComponents<AbilityBase>();
            foreach (var ability in abilities)
            {
                if (ability.Type == OriginSO.Type)
                {
                    _ability = ability;
                }
            }
        }

        public override void OnUpdate() { }

        public override async void OnStateEnter()
        {
            Debug.Log("perform " + OriginSO.Type);
            _character.SetAbilityInput(OriginSO.Type, false);
            await _ability.Perform();
        }

        public override void OnStateExit() { }
    }
}
