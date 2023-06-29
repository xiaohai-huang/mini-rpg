using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Int Event Channel", menuName = "My Scriptable Objects/Event Channel/Int Event Channel")]
public class IntEventChannel : ScriptableObject
{
    public event Action<int> OnEventRaised;

    public void RaiseEvent(int value)
    {
        OnEventRaised?.Invoke(value);
    }
}
