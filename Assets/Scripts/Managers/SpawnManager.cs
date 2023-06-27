using UnityEngine;
using Xiaohai.Input;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    public GameObject EnemyPrefab;
    public Transform SpawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        _inputReader.InputActions.GamePlay.SpawnEnemy.performed += SpawnEnemy_performed;
    }

    private void SpawnEnemy_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        var go = Instantiate(EnemyPrefab);
        go.transform.position = SpawnPoint.position;
    }
}
