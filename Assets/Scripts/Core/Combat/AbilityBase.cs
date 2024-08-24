using System;
using Core.Game.Mana;
using UnityEngine;
using Xiaohai.Character;

namespace Core.Game.Combat
{
    public abstract class AbilityBase : MonoBehaviour
    {
        public abstract string Name { get; }
        public abstract Character.Ability Type { get; }
        public int MaxLevel { get; protected set; }
        public int CurrentLevel { get; private set; } = 0;
        public bool HasEnoughMana => ManaSystem.CurrentMana >= ManaCost;
        public abstract int ManaCost { get; }
        public event Action<bool> OnPerformingChange;
        public bool Performing { get; private set; }
        public virtual bool CanPerform => HasEnoughMana && CurrentLevel != 0;
        protected ManaSystem ManaSystem;
        protected Character Host { get; private set; }

        public virtual void Awake()
        {
            ManaSystem = GetComponent<ManaSystem>();
            Host = GetComponent<Character>();
        }

        protected abstract Awaitable PerformAction();

        public async Awaitable<bool> Perform()
        {
            if (HasEnoughMana && CurrentLevel != 0)
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
            }
        }

        public virtual void ResetLevel() => CurrentLevel = 0;
    }
}
