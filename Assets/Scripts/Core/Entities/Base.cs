using Core.Game.Statistics;
using UnityEngine;
using Xiaohai.Character;

namespace Core.Game.Entities
{
    [RequireComponent(typeof(Damageable))]
    public class Base : Entity
    {
        public StatSystem Statistics;

        [SerializeField]
        private BaseStats _baseStats;
        private Damageable _damageable;
        public Damageable Damageable => _damageable;

        public virtual void Awake()
        {
            Statistics = new StatSystem(_baseStats);
            _damageable = GetComponent<Damageable>();
        }
    }
}
