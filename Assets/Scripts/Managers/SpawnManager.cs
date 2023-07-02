using UnityEngine;
using Xiaohai.Input;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    public GameObject EnemyPrefab;
    public Transform[] SpawnPoints;
    // Start is called before the first frame update
    void Start()
    {
        _inputReader.OnSpawnEnemy += SpawnEnemy_performed;
    }

    private void SpawnEnemy_performed()
    {
        for (int i = 0; i < 3; i++)
        {
            var go = Instantiate(EnemyPrefab);
            var point = SpawnPoints[Random.Range(0, SpawnPoints.Length)];
            go.transform.position = point.position;
            go.SetActive(true);
        }
    }
}
