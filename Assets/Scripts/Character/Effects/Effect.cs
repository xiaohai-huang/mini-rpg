using System;
using UnityEngine;

/// <summary>
/// Effect that can be applied to an object. e.g., Heal Effect
/// </summary>
public abstract class Effect
{
    public EffectSO OriginSO;
    public string Name;

    [NonSerialized]
    public bool Finished;
    public bool ShowInListUI;

    /// <summary>
    /// CoolDown time in ms.
    /// </summary>
    public float CoolDownTime;
    public Sprite Icon;
    public Action OnApplyCallback;
    public Action OnUpdateCallback;
    public Action OnRemoveCallback;
    private Action _CleanUps;

    /// <summary>
    /// Should only be invoked by EffectSystem
    /// </summary>
    /// <param name="system"></param>
    public void OnApplyWrapper(EffectSystem system)
    {
        var cleanup = OnApply(system);
        if (cleanup != null)
        {
            _CleanUps += cleanup;
        }
    }

    public virtual Action OnApply(EffectSystem system)
    {
        Finished = false;
        OnApplyCallback?.Invoke();
        return null;
    }

    public virtual void OnUpdate(EffectSystem system)
    {
        OnUpdateCallback?.Invoke();
    }

    public virtual void OnRemove(EffectSystem system)
    {
        OnRemoveCallback?.Invoke();
        _CleanUps?.Invoke();
        _CleanUps = null;
    }

    public override bool Equals(object obj)
    {
        if (obj is null or not Effect)
        {
            return false;
        }
        Effect other = (Effect)obj;
        return Name == other.Name && OriginSO == other.OriginSO;
    }

    public override int GetHashCode()
    {
        int hash = Name.GetHashCode();
        if (OriginSO != null)
        {
            hash += OriginSO.GetHashCode();
        }
        return hash;
    }
}
