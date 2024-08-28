using System;
using UnityEngine;

namespace Core.Game.Entities
{
    [Serializable]
    public struct LevelInfo
    {
        public int XP;
        public int CumulativeXP;
    }

    [CreateAssetMenu(
        fileName = "Levels Info",
        menuName = "My Scriptable Objects/Heros/Levels Info"
    )]
    public class LevelsInfo : ScriptableDictionary<int, LevelInfo> { }
}
