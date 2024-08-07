using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _text;
    private Animator _animator;
    private static readonly int LEFT_TRIGGER = Animator.StringToHash("Left");
    private static readonly int RIGHT_TRIGGER = Animator.StringToHash("Right");

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void ShowLeft(int damageAmount)
    {
        SetDamage(damageAmount);
        _animator.SetTrigger(LEFT_TRIGGER);
    }

    public void ShowRight(int damageAmount)
    {
        SetDamage(damageAmount);
        _animator.SetTrigger(RIGHT_TRIGGER);
    }

    void OnEnable()
    {
        Destroy(gameObject, 2);
    }

    void SetDamage(int damageAmount)
    {
        _text.text = damageAmount.ToString();
    }
}
