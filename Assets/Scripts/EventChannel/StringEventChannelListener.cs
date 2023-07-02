using UnityEngine;
using UnityEngine.Events;

public class StringEventChannelListener : MonoBehaviour
{
    [SerializeField] private StringEventChannel EventChannel;
    [SerializeField] private UnityEvent<string> Response;

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

    void Listener(string value)
    {
        Response?.Invoke(value);
    }
}
