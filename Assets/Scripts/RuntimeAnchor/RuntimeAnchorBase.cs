using System;
using UnityEngine;
using UnityEngine.Events;

public class RuntimeAnchorBase<T> : ScriptableObject
    where T : UnityEngine.Object
{
    public UnityAction<T> OnProvided;
    private Func<T> _getter;
    private T _value;
    public T Value
    {
        get
        {
            if (_getter != null)
            {
                return _getter();
            }
            return _value;
        }
        private set { _value = value; }
    }

    public void Provide(T value)
    {
        if (value == null)
        {
            Debug.LogError("A null value was provided to the " + this.name + " runtime anchor.");
            return;
        }

        Value = value;
        OnProvided?.Invoke(value);
    }

    public void Provide(Func<T> getter)
    {
        _getter = getter;
    }

    public void Clear()
    {
        Value = null;
    }
}
