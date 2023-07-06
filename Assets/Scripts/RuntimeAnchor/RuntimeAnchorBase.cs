using UnityEngine;
using UnityEngine.Events;

public class RuntimeAnchorBase<T> : ScriptableObject where T : Object
{
    public UnityAction<T> OnProvided;
    public T Value { get; private set; }
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

    public void Clear()
    {
        Value = null;
    }
}
