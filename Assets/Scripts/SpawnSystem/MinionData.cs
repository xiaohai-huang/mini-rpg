using Core.Game.Entities;
using UnityEngine;

namespace Core.Game.SpawnSystem
{
    [CreateAssetMenu(fileName = "Minion", menuName = "Spawn System/Minion Data")]
    /// <summary>
    /// Key: minion id
    /// Value: minion prefab
    /// </summary>
    public class MinionData : ScriptableDictionary<string, Base> { }
}
