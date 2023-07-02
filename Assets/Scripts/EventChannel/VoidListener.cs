using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

public class VoidListener : MonoBehaviour
{
    public MonoBehaviour target;
    public string eventName;
    public UnityEvent response;

    private MethodInfo eventAddMethod;
    private MethodInfo eventRemoveMethod;
    private Delegate eventDelegate;

    void OnEnable()
    {
        if (target != null)
            AttachListener();
    }

    void OnDisable()
    {
        if (target != null)
            DetachListener();
    }

    void AttachListener()
    {
        Type targetType = target.GetType();
        EventInfo targetEvent = targetType.GetEvent(eventName);

        if (targetEvent != null)
        {
            eventAddMethod = targetEvent.GetAddMethod();
            eventRemoveMethod = targetEvent.GetRemoveMethod();
            eventDelegate = Delegate.CreateDelegate(targetEvent.EventHandlerType, this, "Listener");

            eventAddMethod.Invoke(target, new object[] { eventDelegate });
        }
    }

    void DetachListener()
    {
        if (eventRemoveMethod != null && eventDelegate != null)
        {
            eventRemoveMethod.Invoke(target, new object[] { eventDelegate });

            eventAddMethod = null;
            eventRemoveMethod = null;
            eventDelegate = null;
        }
    }

    void Listener()
    {
        response?.Invoke();
    }
}
