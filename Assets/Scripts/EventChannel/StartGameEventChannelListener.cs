using UnityEngine;
using UnityEngine.Events;

public class StartGameEventChannelListener : MonoBehaviour
{
    [SerializeField]
    private StartGameEventChannel EventChannel;

    [SerializeField]
    private UnityEvent<int, int> Response;

    bool _awaked = false;

    void Awake()
    {
        _awaked = true;
    }

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

    void Listener(int heroId, int skinId)
    {
        Response?.Invoke(heroId, skinId);
    }
}
