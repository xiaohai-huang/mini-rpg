using System;
using UnityEngine;

namespace Core.Game.Entities.Heros.XiaoQiao
{
    [Serializable]
    public struct AbilityThreeLevelInfo
    {
        public int DamageAmount;
        public float CoolDown;
        public int ManaCost;
        public int UnlockAtPlayerLevel;
    }

    [CreateAssetMenu(
        fileName = "Ability Three Info",
        menuName = "My Scriptable Objects/Heros/XiaoQiao/Ability Three Info"
    )]
    public class AbilityThreeInfoSO : ScriptableDictionary<int, AbilityThreeLevelInfo> { }
}
