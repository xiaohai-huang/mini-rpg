using System;
using UnityEngine;

namespace Core.Game.Entities.Heros.XiaoQiao
{
    [Serializable]
    public struct AbilityTwoLevelInfo
    {
        public int DamageAmount;
        public float CoolDown;
        public int ManaCost;
    }

    [CreateAssetMenu(
        fileName = "Ability Two Info",
        menuName = "My Scriptable Objects/Heros/XiaoQiao/Ability Two Info"
    )]
    public class AbilityTwoInfoSO : ScriptableDictionary<int, AbilityTwoLevelInfo> { }
}
