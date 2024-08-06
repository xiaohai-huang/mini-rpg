using Core.Game.Statistics;
using UnityEngine;

namespace Core.Game.Entities
{
    public class Base : Entity
    {
        public StatSystem Statistics;

        [SerializeField]
        private BaseStats _baseStats;

        public virtual void Awake()
        {
            Statistics = new StatSystem(_baseStats);
        }
    }
}
