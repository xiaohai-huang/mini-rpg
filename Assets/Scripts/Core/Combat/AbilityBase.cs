using System;
using Core.Game.Mana;
using UnityEngine;
using UnityEngine.Events;
using Xiaohai.Character;

namespace Core.Game.Combat
{
    public abstract class AbilityBase : MonoBehaviour
    {
        public abstract string Name { get; }
        public abstract Character.Ability Type { get; }

        /// <summary>
        /// The first param is current level.
        /// The second param is max level.
        /// </summary>
        public UnityEvent<int, int> OnLevelChange;
        public UnityEvent OnLevelUp;
        public UnityEvent OnLevelReset;
        private int _maxLevel;
        public int MaxLevel
        {
            get => _maxLevel;
            protected set
            {
                _maxLevel = value;
                OnLevelChange?.Invoke(CurrentLevel, value);
            }
        }
        private int _currentLevel;
        public int CurrentLevel
        {
            get => _currentLevel;
            private set
            {
                _currentLevel = value;
                OnLevelChange?.Invoke(CurrentLevel, MaxLevel);
                OnUpgradableChange?.Invoke(Upgradable);
            }
        }
        public int NextLevel => CurrentLevel + 1;
        public bool HasEnoughMana => ManaSystem.CurrentMana >= ManaCost;
        public int ManaCost
        {
            get
            {
                if (ManaSystem.ZeroCooldown)
                    return 0;
                return CurrentLevelManaCost;
            }
        }
        public abstract int CurrentLevelManaCost { get; }
        public event Action<bool> OnPerformingChange;
        public bool Performing { get; private set; }
        public virtual bool CanPerform => HasEnoughMana && CurrentLevel != 0 && CD_Timer <= 0;
        protected ManaSystem ManaSystem;
        protected Character Host { get; private set; }
        public abstract bool Upgradable { get; }
        public UnityEvent<bool> OnUpgradableChange;
        public float CD_Timer { get; protected set; }
        public abstract float Total_CD_Timer { get; }

        public virtual void Awake()
        {
            ManaSystem = GetComponent<ManaSystem>();
            Host = GetComponent<Character>();
        }

        public virtual void Update()
        {
            if (ManaSystem.ZeroCooldown)
            {
                CD_Timer = 0;
                return;
            }

            if (CD_Timer > 0)
            {
                CD_Timer -= Time.deltaTime;
            }
        }

        protected abstract Awaitable PerformAction();

        public async Awaitable<bool> Perform()
        {
            if (CanPerform)
            {
                Performing = true;
                OnPerformingChange?.Invoke(Performing);

                await PerformAction();

                Performing = false;
                OnPerformingChange?.Invoke(Performing);
                return true;
            }
            else
            {
                Debug.LogWarning("Unable to perform the ability", this);
                return false;
            }
        }

        public virtual void LevelUp()
        {
            if (CurrentLevel < MaxLevel)
            {
                CurrentLevel++;
                OnLevelUp?.Invoke();
            }
        }

        public virtual void ResetLevel()
        {
            CurrentLevel = 0;
            CD_Timer = 0;
            OnLevelReset?.Invoke();
        }

        public void HandleAbilityUpgradeCreditsChange(int _)
        {
            OnUpgradableChange?.Invoke(Upgradable);
        }

        public static AbilityBase GetAbility(MonoBehaviour mono, Character.Ability type)
        {
            Type t = null;
            switch (type)
            {
                case Character.Ability.One:
                {
                    t = typeof(AbilityOneBase);
                    break;
                }
                case Character.Ability.Two:
                {
                    t = typeof(AbilityTwoBase);
                    break;
                }
                case Character.Ability.Three:
                {
                    t = typeof(AbilityThreeBase);
                    break;
                }
            }

            return (AbilityBase)mono.GetComponent(t);
        }

        protected void StartCDTimer(float time)
        {
            if (ManaSystem.ZeroCooldown)
            {
                CD_Timer = 0;
                return;
            }
            CD_Timer = time;
        }
    }
}
