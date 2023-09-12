using UnityEngine;


public abstract class EffectSO : ScriptableObject
{
    public string Name;
    public bool ShowInListUI;
    public float CoolDownTime;
    public Sprite Icon;

    public T CreateEffect<T>() where T : Effect, new()
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

public abstract class EffectSO<T> : EffectSO where T : Effect, new()
{
    public T CreateEffect() => CreateEffect<T>();
}

