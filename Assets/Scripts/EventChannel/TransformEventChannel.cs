using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Transform Event Channel", menuName = "My Scriptable Objects/Event Channel/Transform Event Channel")]
public class TransformEventChannel : ScriptableObject
{
    public UnityAction<Transform> OnEventRaised;

    public void RaiseEvent(Transform value)
    {
        OnEventRaised?.Invoke(value);
    }

}
