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
            _effects.Remove(effect);
            effect.OnRemove(this);
        });

        _effectsToRemove.Clear();
    }
}
