using System;
using UnityEngine;

namespace Core.Game.Entities.Heros.XiaoQiao
{
    [Serializable]
    public struct LevelInfo
    {
        public int DamageAmount;
        public float CoolDown;
        public int ManaCost;
    }

    [CreateAssetMenu(
        fileName = "Ability One Info",
        menuName = "My Scriptable Objects/Heros/XiaoQiao/Ability One Info"
    )]
    public class AbilityOneSO : ScriptableDictionary<int, LevelInfo> { }
}
