using System;
using System.Collections.Generic;
using Xiaohai.Character;

namespace Xiaohai.Utilities
{
    public class DamageableTargetSelection : TargetSelection<Damageable>
    {
        public enum SelectionStrategy
        {
            Closest,
            MinimumAbsoluteHeath,
            MinimumPercentageHealth,
            FirstEntered
        }
        private readonly Dictionary<SelectionStrategy, Comparison<TargetInfo<Damageable>>> _comparisons = new();

        void Awake()
        {
            _comparisons[SelectionStrategy.Closest] = (a, b) =>
                (a.GO.transform.position - transform.position).sqrMagnitude.CompareTo(
                (b.GO.transform.position - transform.position).sqrMagnitude);

            _comparisons[SelectionStrategy.MinimumAbsoluteHeath] = (a, b) =>
                a.target.CurrentHealth - b.target.CurrentHealth;

            _comparisons[SelectionStrategy.MinimumPercentageHealth] = (a, b) =>
                (a.target.CurrentHealth / (float)a.target.MaxHealth).CompareTo(
                 b.target.CurrentHealth / (float)b.target.MaxHealth);

            _comparisons[SelectionStrategy.FirstEntered] = (a, b) =>
                a.EnteredTime.CompareTo(b.EnteredTime);
        }

        public Damageable GetTarget(SelectionStrategy strategy)
        {
            if (Targets.Count == 0) return null;

            var array = Targets.ToArray();
            Array.Sort(array, _comparisons[strategy]);

            // The target should be alive
            Damageable target = null;
            for (int i = 0; i < array.Length; i++)
            {
                if (!array[i].target.IsDead)
                {
                    target = array[i].target;
                    break;
                }
            }
            return target;
        }
    }
}