using System;
using UnityEngine;

namespace Xiaohai.Character
{
    public class Health : MonoBehaviour
    {
        public int MaxHealth;
        public int CurrentHealth;

        public void ReduceHealth(int healthAmount)
        {
            if (CurrentHealth == 0)
                return;
            CurrentHealth = Math.Max(CurrentHealth - healthAmount, 0);
        }

        public void IncreaseHealth(int healthAmount)
        {
            CurrentHealth = Math.Clamp(CurrentHealth + healthAmount, 0, MaxHealth);
        }
    }
}
