using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Void Event Channel", menuName = "My Scriptable Objects/Event Channel/Void Event Channel")]
public class VoidEventChannel : ScriptableObject
{
    public event Action OnEventRaised;

    public void RaiseEvent()
    {
        OnEventRaised?.Invoke();
    }
}
