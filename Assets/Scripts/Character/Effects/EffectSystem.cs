using System.Collections.Generic;
using UnityEngine;
using Xiaohai.Character;

public class EffectSystem : MonoBehaviour
{
    private readonly HashSet<Effect> _effects = new();
    private readonly List<Effect> _effectsToRemove = new();

    private Damageable _damageable;
    public void AddEffect(Effect effect)
    {
        _effects.Add(effect);
        effect.OnApply(this);
    }

    public void RemoveEffect(Effect effect)
    {
        _effectsToRemove.Add(effect);
    }

    public bool Contains(Effect effect)
    {
        return _effects.Contains(effect);
    }

    public void RestoreHealth(int healthToAdd)
    {
        // TODO: add checks for health reduction effects. e.g., 梦魇，制裁
        _damageable.RestoreHealth(healthToAdd);
    }

    public void DealDamage(int damageAmount)
    {
        _damageable.TakeDamage(damageAmount);
    }

    void Awake()
    {
        _damageable = GetComponent<Damageable>();
    }

    // Update is called once per frame
    void Update()
    {
        _effectsToRemove.ForEach(effect =>
        {
            bool success = _effects.Remove(effect);
            if (success) effect.OnRemove(this);
        });

        foreach (Effect effect in _effects)
        {
            if (effect.Finished)
            {
                _effectsToRemove.Add(effect);
            }
            else
            {
                effect.OnUpdate(this);
            }
        }

        _effectsToRemove.ForEach(effect =>
        {
            bool success = _effects.Remove(effect);
            if (success) effect.OnRemove(this);
        });

        _effectsToRemove.Clear();
    }
}
