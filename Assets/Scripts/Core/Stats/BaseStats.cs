using UnityEngine;

namespace Core.Game.Statistics
{
    [CreateAssetMenu(
        fileName = "BaseStats",
        menuName = "My Scriptable Objects/Statistics/BaseStats"
    )]
    public class BaseStats : ScriptableDictionary<StatType, float> { }
}
