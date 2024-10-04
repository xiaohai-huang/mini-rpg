using System;
using System.Collections.Generic;
using Core.Game.Combat;
using Core.Game.Entities;
using Core.Game.Mana;
using UnityEngine;
using Xiaohai.Character;

public class EffectSystem : MonoBehaviour
{
    public Effect[] Effects
    {
        get
        {
            var temp = new Effect[_effects.Count];
            _effects.CopyTo(temp, 0);
            return temp;
        }
    }
    public Action<Effect> OnAddEffect;
    public Action<Effect> OnRemoveEffect;
    private readonly HashSet<Effect> _effects = new();
    private readonly List<Effect> _effectsToRemove = new();

    private Damageable _damageable;
    private ManaSystem _manaSystem;
    public Base Host { get; private set; }

    void Awake()
    {
        _damageable = GetComponent<Damageable>();
        _manaSystem = GetComponent<ManaSystem>();
        Host = GetComponent<Base>();
    }

    public void AddEffect(Effect newEffect)
    {
        if (Contains(newEffect))
        {
            Effect oldEffect = Array.Find(Effects, (effect) => effect.Name == newEffect.Name);
            bool success = _effects.Remove(newEffect);
            if (success)
            {
                oldEffect.OnRemove(this);
                OnRemoveEffect?.Invoke(oldEffect);
            }
        }
        _effects.Add(newEffect);
        newEffect.OnApplyWrapper(this);
        OnAddEffect?.Invoke(newEffect);
    }

    public void RemoveEffect(Effect effect)
    {
        _effectsToRemove.Add(effect);
    }

    public void RemoveEffect(EffectSO effectSO)
    {
        var effect = Find(effectSO);
        if (effect != null)
        {
            RemoveEffect(effect);
        }
    }

    public Effect Find(EffectSO effectSO)
    {
        foreach (var effect in _effects)
        {
            if (effect.OriginSO == effectSO)
            {
                return effect;
            }
        }
        return null;
    }

    public bool Contains(Effect effect)
    {
        return _effects.Contains(effect);
    }

    public bool Contains(EffectSO effectSO)
    {
        return Find(effectSO) != null;
    }

    public void RestoreHealth(int healthToAdd)
    {
        // TODO: add checks for health reduction effects. e.g., 梦魇，制裁
        _damageable.RestoreHealth(healthToAdd);
    }

    public void DealDamage(Damage dmg)
    {
        _damageable.TakeDamage(dmg);
    }

    public void RecoverMana(int manaToAdd)
    {
        _manaSystem.RecoverMana(manaToAdd);
    }

    public void ConsumeMana(int amount)
    {
        _manaSystem.Consume(amount);
    }

    // Update is called once per frame
    void Update()
    {
        _effectsToRemove.ForEach(effect =>
        {
            bool success = _effects.Remove(effect);
            if (success)
            {
                effect.OnRemove(this);
                OnRemoveEffect?.Invoke(effect);
            }
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
            if (success)
            {
                effect.OnRemove(this);
                OnRemoveEffect?.Invoke(effect);
            }
        });

        _effectsToRemove.Clear();
    }
}
