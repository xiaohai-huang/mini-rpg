using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IntEventListener : MonoBehaviour
{
    [SerializeField] private IntEventChannel EventChannel;
    public UnityEvent<int> Response;

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

    void Listener(int value)
    {
        Response?.Invoke(value);
    }
}
