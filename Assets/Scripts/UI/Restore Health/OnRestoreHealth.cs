using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnRestoreHealth : MonoBehaviour
{
    [SerializeField] private RestoreHealthText _restoreTextPrefab;
    public void HandleRestoreHealth(int health)
    {
        var text = Instantiate(_restoreTextPrefab, transform);
        text.SetHealth(health);
    }
}
