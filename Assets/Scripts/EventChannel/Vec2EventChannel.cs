using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(
    fileName = "Vector2 Event Channel",
    menuName = "My Scriptable Objects/Event Channel/Vector2 Event Channel"
)]
public class Vec2EventChannel : ScriptableObject
{
    public event UnityAction<Vector2> OnEventRaised;

    public void RaiseEvent(Vector2 value)
    {
        OnEventRaised?.Invoke(value);
    }
}
