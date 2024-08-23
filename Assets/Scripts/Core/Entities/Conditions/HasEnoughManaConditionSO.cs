using Core.Game.Mana;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

namespace Core.Game.Entities.Conditions
{
    [CreateAssetMenu(
        fileName = "Has Enough Mana Condition",
        menuName = "State Machines/Conditions/Has Enough Mana Condition"
    )]
    public class HasEnoughManaConditionSO : StateConditionSO
    {
        public int Mana;

        protected override Condition CreateCondition() => new HasEnoughManaCondition();
    }

    public class HasEnoughManaCondition : Condition
    {
        private ManaSystem _manaSystem;
        protected new HasEnoughManaConditionSO OriginSO => (HasEnoughManaConditionSO)base.OriginSO;

        public override void Awake(StateMachine stateMachine)
        {
            _manaSystem = stateMachine.GetComponent<ManaSystem>();
        }

        protected override bool Statement()
        {
            return _manaSystem.CurrentMana >= OriginSO.Mana;
        }
    }
}
