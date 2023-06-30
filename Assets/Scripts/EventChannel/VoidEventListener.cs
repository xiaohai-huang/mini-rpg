using UnityEngine;
using UnityEngine.Events;

public class VoidEventListener : MonoBehaviour
{

    [SerializeField] private VoidEventChannel EventChannel;
    [SerializeField] private UnityEvent Response;

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

    void Listener()
    {
        Response?.Invoke();
    }
}
