using UnityEngine;

namespace Core.Game
{
    [CreateAssetMenu(fileName = "Base Stats", menuName = "My Scriptable Objects/Config/Base Stats")]
    public class BaseStats : ScriptableObject
    {
        public float MaxHealth;

        /// <summary>
        /// The rate at which a champion regains health over time.
        /// e.g., XiaoQiao regenerates 7 health per second at level 1.
        /// </summary>
        public float HealthRegeneration;
        public float MaxMana;
        public float ManaRegeneration;
        public float PhysicalDamage;
        public float MagicDamage;
        public float TrueDamage;
        public float Armor;
        public float MagicResistance;

        /// <summary>
        /// Miss Fortune has an attack speed of 0.679 attacks per second at level 1.
        /// </summary>
        public float AttackSpeed;
        public float MoveSpeed;
    }
}
