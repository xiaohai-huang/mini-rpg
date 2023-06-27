using System;
using UnityEngine;
using Xiaohai.UI;

public class HP : MonoBehaviour
{
    public int MaxHP;
    public int CurrentHP;
    [SerializeField] private ProgressBar _healthBar;

    private void Awake()
    {
        CurrentHP = MaxHP;
    }

    void Start()
    {
        _healthBar.SetProgress(CurrentHP / MaxHP);
    }

    public void ReduceHP(int hp)
    {
        if (CurrentHP == 0) return;
        CurrentHP -= hp;
        CurrentHP = Math.Max(CurrentHP, 0);
        _healthBar.SetProgress((float)CurrentHP / MaxHP);
    }


}
