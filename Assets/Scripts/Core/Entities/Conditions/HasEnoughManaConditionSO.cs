using Core.Game.Combat;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using Xiaohai.Character;

namespace Core.Game.Entities.Conditions
{
    [CreateAssetMenu(
        fileName = "Has Enough Mana Condition",
        menuName = "State Machines/Conditions/Has Enough Mana Condition"
    )]
    public class HasEnoughManaConditionSO : StateConditionSO
    {
        public Character.Ability Type;

        protected override Condition CreateCondition() => new HasEnoughManaCondition();
    }

    public class HasEnoughManaCondition : Condition
    {
        private AbilityBase _ability;

        protected new HasEnoughManaConditionSO OriginSO => (HasEnoughManaConditionSO)base.OriginSO;

        public override void Awake(StateMachine stateMachine)
        {
            var abilities = stateMachine.GetComponents<AbilityBase>();
            foreach (var ability in abilities)
            {
                if (ability.Type == OriginSO.Type)
                {
                    _ability = ability;
                }
            }
        }

        protected override bool Statement()
        {
            return _ability.HasEnoughMana;
        }
    }
}
