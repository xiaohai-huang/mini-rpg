using System;
using Core.Game.Common;
using Core.Game.Statistics;

namespace Core.Game.Combat
{
    public static class Calculator
    {
        /// <summary>
        /// Final Damage = initial damage X (1 - Reduction Rate)
        /// </summary>
        /// <param name="initialDamageAmount"></param>
        /// <param name="sender">Attacker</param>
        /// <param name="receiver">Victim</param>
        /// <returns>The actual damage that should apply on the victim</returns>
        public static float GetDamageAmount(
            float initialDamageAmount,
            DamageType damageType,
            StatSystem sender,
            StatSystem receiver
        )
        {
            float reductionRate = GetReductionRate(damageType, sender, receiver);
            float finalDamageAmount = initialDamageAmount * (1 - reductionRate);
            return finalDamageAmount;
        }

        private static float GetReductionRate(
            DamageType damageType,
            StatSystem sender,
            StatSystem receiver
        )
        {
            float actualResistance = GetActualResistance(damageType, sender, receiver);
            return actualResistance / (actualResistance + Constants.DAMAGE_FORMULA_COEFFICIENT);
        }

        private static float GetActualResistance(
            DamageType damageType,
            StatSystem sender,
            StatSystem receiver
        )
        {
            float senderFlatPenetration,
                senderPercentPenetration,
                receiverResistance;

            if (damageType == DamageType.Physical)
            {
                senderFlatPenetration = sender
                    .GetStat(StatType.FlatPhysicalResistancePenetration)
                    .ComputedValue;
                senderPercentPenetration = sender
                    .GetStat(StatType.PercentagePhysicalResistancePenetration)
                    .ComputedValue;
                receiverResistance = receiver.GetStat(StatType.PhysicalResistance).ComputedValue;
            }
            else if (damageType == DamageType.Magical)
            {
                senderFlatPenetration = sender
                    .GetStat(StatType.FlatMagicalResistancePenetration)
                    .ComputedValue;
                senderPercentPenetration = sender
                    .GetStat(StatType.PercentageMagicalResistancePenetration)
                    .ComputedValue;
                receiverResistance = receiver.GetStat(StatType.MagicalResistance).ComputedValue;
            }
            else
            {
                throw new ArgumentException("Invalid damage type.");
            }

            return (receiverResistance - senderFlatPenetration) * (1 - senderPercentPenetration);
        }
    }
}
