using UnityEngine;

namespace Core.Game.Statistics
{
    [CreateAssetMenu(
        fileName = "BaseStats",
        menuName = "My Scriptable Objects/Statistics/BaseStats"
    )]
    public class BaseStatsSO : ScriptableDictionary<StatType, float> { }
}
