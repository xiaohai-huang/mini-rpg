using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class BoolEvent : UnityEvent<bool, GameObject> { }

public class ZoneTrigger : MonoBehaviour
{
    [SerializeField] private BoolEvent _enterZone;

    private void OnTriggerEnter(Collider other)
    {
        _enterZone?.Invoke(true, other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        _enterZone?.Invoke(false, other.gameObject);
    }
}
