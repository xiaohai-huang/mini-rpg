using UnityEngine;
using Xiaohai.UI;

public class HP : MonoBehaviour
{
    public float MaxHP;
    public float CurrentHP;
    [SerializeField] private ProgressBar _healthBar;

    void Start()
    {
        CurrentHP = MaxHP;
        _healthBar.SetProgress(CurrentHP / MaxHP);
    }

    public void ReduceHP(float hp)
    {
        if (CurrentHP == 0) return;
        CurrentHP -= hp;
        CurrentHP = Mathf.Max(CurrentHP, 0);
        _healthBar.SetProgress(CurrentHP / MaxHP);
    }


}
