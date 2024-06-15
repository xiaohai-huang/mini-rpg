using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Game
{
    public enum StatType
    {
        MaxHealth,
        HealthRegeneration,
        MagicDamage,
        MoveSpeed
    }

    public class Stats : MonoBehaviour
    {
        [SerializeField]
        private BaseStats _baseStats;
        public Dictionary<StatType, StatMediator> Mediators { get; private set; }

        void Awake()
        {
            Mediators = new()
            {
                { StatType.MaxHealth, new StatMediator(_baseStats.MaxHealth) },
                { StatType.HealthRegeneration, new StatMediator(_baseStats.HealthRegeneration) },
                { StatType.MagicDamage, new StatMediator(_baseStats.MagicDamage) },
                { StatType.MoveSpeed, new StatMediator(_baseStats.MoveSpeed) }
            };
        }

        void Update()
        {
            foreach (var mediator in Mediators.Values)
            {
                mediator.Update(Time.deltaTime);
            }
        }

        public float MaxHealth => Mediators[StatType.MaxHealth].Stat;

        public float HealthRegeneration => 0;

        public float MagicDamage => 0;

        public float MoveSpeed => 0;
    }
}
