using System;
using Core.Game.Common;
using Core.Game.Entities;
using Core.Game.Statistics;

namespace Core.Game.Combat
{
    public class Damage
    {
        private readonly StatSystem _sender;
        private readonly StatSystem _receiver;
        private readonly DamageType _type;
        private readonly float _baseDamageAmount;
        public float ComputedValue => GetDamageAmount();

        public Damage(Base sender, Base receiver, DamageType type, float baseDamageAmount)
        {
            _sender = sender.Statistics;
            _receiver = receiver.Statistics;
            _type = type;
            _baseDamageAmount = baseDamageAmount;
        }

        public Damage(
            StatSystem sender,
            StatSystem receiver,
            DamageType type,
            float baseDamageAmount
        )
        {
            _sender = sender;
            _receiver = receiver;
            _type = type;
            _baseDamageAmount = baseDamageAmount;
        }

        private float GetDamageAmount()
        {
            float reductionRate = GetReductionRate(_type, _sender, _receiver);
            float finalDamageAmount = _baseDamageAmount * (1 - reductionRate);
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
