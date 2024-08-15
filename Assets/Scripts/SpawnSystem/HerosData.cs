using UnityEngine;
using Xiaohai.Character;

namespace Core.Game.SpawnSystem
{
    [CreateAssetMenu(fileName = "Heros", menuName = "Spawn System/Heros Data")]
    /// <summary>
    /// Key: <hero_id>-<skin_id>
    /// Value: hero prefab
    /// </summary>
    public class HerosData : ScriptableDictionary<string, Character> { }
}
