using System.Collections.Generic;

namespace Core.Game
{
    public class StatMediator
    {
        private readonly float _baseStat;
        public float Stat { get; private set; }
        private readonly LinkedList<StatModifier> _modifiers = new();

        public StatMediator(float baseStat)
        {
            _baseStat = baseStat;
        }

        public void AddModifier(StatModifier modifier)
        {
            _modifiers.AddLast(modifier);
            modifier.OnAdd(this);
        }

        public void RemoveModifier(StatModifier modifier)
        {
            _modifiers.Remove(modifier);
            modifier.OnRemove();
        }

        public float GetEffect()
        {
            float effect = 0;
            var node = _modifiers.First;
            while (node != null)
            {
                effect += node.Value.GetEffect();
                node = node.Next;
            }
            return effect;
        }

        public void Update(float deltaTime)
        {
            var node = _modifiers.First;
            Stat = _baseStat;
            while (node != null)
            {
                if (node.Value.MarkedForRemove)
                {
                    RemoveModifier(node.Value);
                }
                else
                {
                    node.Value.OnUpdate(deltaTime);
                }
                var effect = node.Value.GetEffect();
                Stat += effect;

                node = node.Next;
            }
        }

        // modifiers:
        // 1. +200
        // 2. +20%
        // 3. -30%
        // 4. reduce 10% of the overall health every second

        // get the effects of all modifiers for the given stat type
    }
}
