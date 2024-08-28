using System;
using UnityEngine;
using UnityEngine.Events;

namespace Core.Game.Entities
{
    public class Level : MonoBehaviour
    {
        public int Max { get; private set; }
        public int Value { get; private set; }
        public int XP;
        public UnityEvent<int, int> OnChange;
        public UnityEvent<float> OnXPChange;
        public UnityEvent<int> OnAbilityUpgradeCreditsChange;
        private int _abilityUpgradeCredits;
        public int AbilityUpgradeCredits
        {
            get => _abilityUpgradeCredits;
            private set
            {
                _abilityUpgradeCredits = Math.Clamp(value, 0, Max);
                OnAbilityUpgradeCreditsChange?.Invoke(_abilityUpgradeCredits);
            }
        }

        void Start()
        {
            Max = 15;
            Upgrade();
        }

        public void Upgrade()
        {
            Value = Math.Clamp(Value + 1, 1, Max);
            OnChange?.Invoke(Value, Max);
            AbilityUpgradeCredits++;
        }

        public void SetMax(int newMax)
        {
            Max = newMax;
            OnChange?.Invoke(Value, Max);
        }

        public void HandleAbilityUpgrade()
        {
            AbilityUpgradeCredits--;
        }

        public void HandleAbilityReset()
        {
            AbilityUpgradeCredits = 0;
        }
    }
}
