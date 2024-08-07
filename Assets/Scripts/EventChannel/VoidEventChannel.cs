using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(
    fileName = "Void Event Channel",
    menuName = "My Scriptable Objects/Event Channel/Void Event Channel"
)]
public class VoidEventChannel : ScriptableObject
{
    public UnityAction OnEventRaised;

    public void RaiseEvent()
    {
        OnEventRaised?.Invoke();
    }
}
