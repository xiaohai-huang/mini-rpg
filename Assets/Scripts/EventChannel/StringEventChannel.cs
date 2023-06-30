using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "String Event Channel", menuName = "My Scriptable Objects/Event Channel/String Event Channel")]
public class StringEventChannel : ScriptableObject
{
    public UnityAction<string> OnEventRaised;

    public void RaiseEvent(string value)
    {
        OnEventRaised?.Invoke(value);
    }
}
