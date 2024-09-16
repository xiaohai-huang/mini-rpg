using System;
using Core.Game.Combat;
using UnityEngine;
using UnityEngine.Events;

namespace Core.Game.Entities
{
    public class Level : MonoBehaviour
    {
        /// <summary>
        /// The maximum level
        /// </summary>
        public int Max { get; private set; }

        /// <summary>
        /// Current level
        /// </summary>
        public int Value { get; private set; }

        /// <summary>
        /// Next level
        /// </summary>
        public int Next => Math.Min(Max, Value + 1);

        /// <summary>
        /// Cumulative xp
        /// </summary>
        private int _xp;

        /// <summary>
        /// XP in percentage
        /// </summary>
        public float XP_Percent => (float)(_xp - _data[Value].CumulativeXP) / _data[Next].XP;

        /// <summary>
        /// The current level change, or on max level change
        /// </summary>
        public UnityEvent<int, int> OnChange;
        public UnityEvent<float> OnXPChange;
        public UnityEvent<int> OnAbilityUpgradeCreditsChange;

        [SerializeField]
        private LevelsInfo _data;
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
            Upgrade(clearExceedXP: true);
        }

        public void Upgrade(bool clearExceedXP = false)
        {
            int old = Value;
            Value = Math.Clamp(Value + 1, 1, Max);
            if (old < Value)
            {
                AbilityUpgradeCredits++;
                if (clearExceedXP)
                {
                    // have not tested
                    SetXP(_data[Value].CumulativeXP);
                }
                OnChange?.Invoke(Value, Max);
            }
        }

        public void Reset()
        {
            Value = 0;
            AbilityUpgradeCredits = 0;
            // Reset the level of abilities
            foreach (var ability in GetComponents<AbilityBase>())
            {
                ability.ResetLevel();
            }
            Upgrade(clearExceedXP: true);
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

        public void IncreaseXP(int xp)
        {
            SetXP(prev => prev + xp);
        }

        void SetXP(int newXP)
        {
            _xp = Math.Clamp(newXP, 0, _data[Max].CumulativeXP);
            if (_xp >= _data[Next].CumulativeXP)
            {
                Upgrade();
            }
            OnXPChange?.Invoke(XP_Percent);
        }

        void SetXP(Func<int, int> cb) => SetXP(cb(_xp));
    }
}
