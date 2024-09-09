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
            _ability = AbilityBase.GetAbility(stateMachine, OriginSO.Type);
        }

        protected override bool Statement()
        {
            return _ability.HasEnoughMana;
        }
    }
}
