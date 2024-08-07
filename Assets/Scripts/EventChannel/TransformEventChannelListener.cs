using UnityEngine;
using UnityEngine.Events;

public class TransformEventChannelListener : MonoBehaviour
{
    [SerializeField]
    private TransformEventChannel EventChannel;

    [SerializeField]
    private UnityEvent<Transform> Response;

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

    void Listener(Transform value)
    {
        Response?.Invoke(value);
    }
}
