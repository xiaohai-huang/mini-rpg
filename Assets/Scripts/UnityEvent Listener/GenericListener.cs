using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

public class GenericListener<T> : MonoBehaviour
{
    public MonoBehaviour Target;
    public string EventName;
    public UnityEvent<T> Response;
    private FieldInfo _eventField;
    private UnityEvent<T> _targetEvent;

    void Awake()
    {
        _eventField = Target.GetType().GetField(EventName);
    }

    void OnEnable()
    {
        if (Target != null)
            AttachListener();
    }

    void OnDisable()
    {
        if (Target != null)
            DetachListener();
    }

    void AttachListener()
    {
        _targetEvent = _eventField.GetValue(Target) as UnityEvent<T>;
        _targetEvent?.AddListener(Listener);
    }

    void DetachListener()
    {
        _targetEvent?.RemoveListener(Listener);
    }

    void Listener(T value)
    {
        Response?.Invoke(value);
    }
}
