using System;
using System.Collections.Generic;
using UnityEngine;

namespace Xiaohai.Character
{
    public class Character : MonoBehaviour
    {
        [NonSerialized] public Vector3 Velocity;
        [NonSerialized] public bool AttackInput;
        [NonSerialized] public Vector2 HorizontalInput;
        [NonSerialized] public bool AbilityOneInput;
        [NonSerialized] public Vector2 AbilityOneDirection;
        [NonSerialized] public bool AbilityTwoInput;
        [NonSerialized] public Vector2 AbilityTwoDirection;
        [NonSerialized] public bool AbilityThreeInput;
        [NonSerialized] public Vector2 AbilityThreeDirection;


        public enum Ability
        {
            One,
            Two,
            Three,
            Four
        }

        public bool HasAbilityInput(Ability ability)
        {
            switch (ability)
            {
                case Ability.One:
                    return AbilityOneInput;
                case Ability.Two:
                    return AbilityTwoInput;
                case Ability.Three:
                    return AbilityThreeInput;
                case Ability.Four:
                    break;
            }
            return false;
        }

        private readonly Collider[] colliders = new Collider[20];
        public List<Damageable> GetNearByDamageables(float radius)
        {
            Physics.OverlapSphereNonAlloc(transform.position, radius, colliders);
            List<Damageable> damageables = new();
            foreach (Collider collider in colliders)
            {
                if (collider != null)
                {
                    if (!collider.CompareTag("Player") && collider.TryGetComponent(out Damageable damageable))
                    {
                        damageables.Add(damageable);
                    }
                }
            }
            return damageables;
        }
    }

}
