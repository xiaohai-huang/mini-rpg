using UnityEngine;

public class RestoreHealthSpawner : MonoBehaviour
{
    [SerializeField]
    private RestoreHealthText _restoreTextPrefab;

    public void HandleRestoreHealth(int health)
    {
        var text = Instantiate(_restoreTextPrefab, transform);
        text.SetHealth(health);
        text.gameObject.SetActive(true);
    }
}
