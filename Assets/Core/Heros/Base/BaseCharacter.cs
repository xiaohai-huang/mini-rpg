using System;
using UnityEngine;

namespace Core.Game
{
    public class BaseCharacter : Entity
    {
        private Stats _stats;

        [TextArea(10, 20)]
        public string StatsText;

        void Awake()
        {
            _stats = GetComponent<Stats>();
        }

        void Start()
        {
            var tenPercent = new AddTenPercentModifier();
            _stats.Mediators[StatType.Health].AddModifier(tenPercent);
            _stats.Mediators[StatType.Health].AddModifier(new AddFiveHundredModifier());

            Timer.Instance.SetTimeout(
                () =>
                {
                    _stats.Mediators[StatType.Health].RemoveModifier(tenPercent);
                },
                4000f
            );
        }

        void Update()
        {
            string stats = "";
            foreach (StatType type in Enum.GetValues(typeof(StatType)))
            {
                stats +=
                    $"{type}: {_stats.Mediators[type].Stat} ( {_stats.Mediators[type].GetEffect()} )\n";
            }
            StatsText = stats;
        }
    }
}
