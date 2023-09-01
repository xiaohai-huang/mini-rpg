using UnityEngine;


public abstract class EffectSO<T> : ScriptableObject where T : Effect, new()
{
    public string Name;
    public bool ShowInListUI;
    public float CoolDownTime;
    public Sprite Icon;

    public T CreateEffect()
    {
        var effect = new T
        {
            OriginSO = this,
            Name = Name,
            ShowInListUI = ShowInListUI,
            CoolDownTime = CoolDownTime,
            Icon = Icon

        };
        return effect;
    }
}

