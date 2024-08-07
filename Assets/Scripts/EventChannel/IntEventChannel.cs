using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(
    fileName = "Int Event Channel",
    menuName = "My Scriptable Objects/Event Channel/Int Event Channel"
)]
public class IntEventChannel : ScriptableObject
{
    public event UnityAction<int> OnEventRaised;

    public void RaiseEvent(int value)
    {
        OnEventRaised?.Invoke(value);
    }
}
