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
        _inputReader.OnSpawnEnemy += SpawnEnemy_performed;
    }

    private void SpawnEnemy_performed()
    {
        var go = Instantiate(EnemyPrefab);
        go.transform.position = SpawnPoint.position;
    }
}
