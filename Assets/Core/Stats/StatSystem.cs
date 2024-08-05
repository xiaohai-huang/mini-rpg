using System.Collections.Generic;

namespace Core.Game
{
    public class StatSystemA : IStatSystem
    {
        public void AttachModifier(string statName, Modifier modifier)
        {
            throw new System.NotImplementedException();
        }

        public float GetValue(string name)
        {
            throw new System.NotImplementedException();
        }

        public void Initialize(string initialStats)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveModifier(string statName, Modifier modifier)
        {
            throw new System.NotImplementedException();
        }
    }

    public class Modifier
    {
        public enum ModifierType
        {
            Flat,
            Percentage,
        }

        public ModifierType Type { get; }
        public float Value { get; }
    }

    public class Stat
    {
        readonly List<Modifier> _modifiers;
        public float InitialValue { get; private set; }
        public float Value { get; private set; }

        public Stat(float initialValue)
        {
            _modifiers = new();
            InitialValue = initialValue;
            Value = initialValue;
        }

        public void AddModifier(Modifier modifier)
        {
            _modifiers.Add(modifier);
            Update();
        }

        public void RemoveModifier(Modifier modifier)
        {
            _modifiers.Remove(modifier);
            Update();
        }

        private void Update()
        {
            Value = InitialValue;
            foreach (Modifier modifier in _modifiers)
            {
                switch (modifier.Type)
                {
                    case Modifier.ModifierType.Flat:
                    {
                        Value += modifier.Value;
                        break;
                    }
                    case Modifier.ModifierType.Percentage:
                    {
                        Value *= 1 + modifier.Value;
                        break;
                    }
                }
            }
        }
    }

    public class TestCharacter
    {
        readonly Stat MaxHealth;
        readonly Stat PhysicalDamage;
        readonly Stat MoveSpeed;

        public TestCharacter()
        {
            MaxHealth = new Stat(3500);
            PhysicalDamage = new Stat(168);
            MoveSpeed = new Stat(360);
        }

        public static void Main()
        {
            var XiaoQiao = new TestCharacter();
            // equip a pair of shoes which can increase speed by +60

            // level up: increase health by +10%, 30 physical damage
        }
    }
}
