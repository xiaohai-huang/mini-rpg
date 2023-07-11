using System;
using UnityEngine;

public abstract class Effect
{
    public string Name;
    public bool Finished;
    public virtual void OnApply(EffectSystem system) { }
    public virtual void OnUpdate(EffectSystem system) { }
    public virtual void OnRemove(EffectSystem system) { }

    public override bool Equals(object obj)
    {
        if (obj == null || !(obj is Effect))
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