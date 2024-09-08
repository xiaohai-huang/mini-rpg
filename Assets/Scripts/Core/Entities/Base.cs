using Core.Game.Statistics;
using UnityEngine;
using Xiaohai.Character;

namespace Core.Game.Entities
{
    [RequireComponent(typeof(Damageable))]
    public class Base : Entity
    {
        [SerializeField]
        private BaseStatsSO _baseStats;
        public StatSystem Statistics { get; private set; }
        public Damageable Damageable { get; private set; }
        public Level Level { get; private set; }

        public virtual void Awake()
        {
            Statistics = new StatSystem(_baseStats);
            Damageable = GetComponent<Damageable>();
            Level = GetComponent<Level>();
        }
    }
}
