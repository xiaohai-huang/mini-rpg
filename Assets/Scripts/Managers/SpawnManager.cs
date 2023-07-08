using Unity.Cinemachine;
using UnityEngine;
using Xiaohai.Input;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    public GameObject EnemyPrefab;
    public Character PlayerPrefab;
    public Transform PlayerSpawnPoint;
    public Transform[] SpawnPoints;
    [SerializeField] private TransformEventChannel _playerSpawnedEventChannel;
    [SerializeField] private RuntimeTransformAnchor _playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        _inputReader.OnSpawnEnemy += SpawnEnemy_performed;
        SpawnPlayer();
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

    private void SpawnPlayer()
    {
        Character player = Instantiate(PlayerPrefab, PlayerSpawnPoint.position, PlayerSpawnPoint.rotation);
        player.gameObject.SetActive(true);
        _playerTransform.Provide(player.transform);
        _playerSpawnedEventChannel.RaiseEvent(player.transform);
    }
}
