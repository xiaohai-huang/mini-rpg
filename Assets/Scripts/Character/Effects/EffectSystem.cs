using System.Collections.Generic;
using UnityEngine;
using Xiaohai.Character;

public class EffectSystem : MonoBehaviour
{
    public HashSet<Effect> Effects = new HashSet<Effect>();
    private Damageable _damageable;
    public void AddEffect(Effect effect)
    {
        Effects.Add(effect);
        effect.OnApply(this);
    }

    public void RemoveEffect(Effect effect)
    {
        Effects.Remove(effect);
        effect.OnRemove(this);
    }

    public void RestoreHealth(int healthToAdd)
    {
        // TODO: add checks for health reduction effects. e.g., 梦魇，制裁
        _damageable.RestoreHealth(healthToAdd);
    }

    void Awake()
    {
        _damageable = GetComponent<Damageable>();
    }

    // Update is called once per frame
    void Update()
    {
        Effects.RemoveWhere(effect => effect.Finished);
        foreach (Effect effect in Effects)
        {
            effect.OnUpdate(this);
        }
    }
}
