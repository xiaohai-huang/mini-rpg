using UnityEngine;
using UnityEngine.Events;

public class CreateMinionEventChannelListener : MonoBehaviour
{
    [SerializeField]
    private CreateMinionEventChannel EventChannel;

    [SerializeField]
    private UnityEvent<string, string, Transform> Response;

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

    void Listener(string id, string team, Transform spawnPoint)
    {
        Response?.Invoke(id, team, spawnPoint);
    }
}
