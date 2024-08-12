using UnityEngine;
using UnityEngine.Events;

public class Vec2EventChannelListener : MonoBehaviour
{
    [SerializeField]
    private Vec2EventChannel EventChannel;

    [SerializeField]
    private UnityEvent<Vector2> Response;

    void OnEnable()
    {
        if (EventChannel != null)
            EventChannel.OnEventRaised += Listener;
    }

    void OnDisable()
    {
        if (EventChannel != null)
            EventChannel.OnEventRaised -= Listener;
    }

    void Listener(Vector2 value)
    {
        Response?.Invoke(value);
    }
}
