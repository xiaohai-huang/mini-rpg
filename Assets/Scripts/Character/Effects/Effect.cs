using System;
using UnityEngine;

/// <summary>
/// Effect that can be applied to an object. e.g., Heal Effect
/// </summary>
public abstract class Effect
{
    public string Name;
    public bool Finished;
    public Action OnApplyCallback;
    public Action OnUpdateCallback;
    public Action OnRemoveCallback;
    public virtual void OnApply(EffectSystem system) { OnApplyCallback?.Invoke(); }
    public virtual void OnUpdate(EffectSystem system) { OnUpdateCallback?.Invoke(); }
    public virtual void OnRemove(EffectSystem system) { OnRemoveCallback?.Invoke(); }

    public override bool Equals(object obj)
    {
        if (obj is null or not Effect)
        {
            return false;
        }
        Effect other = (Effect)obj;
        return Name == other.Name;
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}