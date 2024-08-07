using System;
using UnityEngine;
using Xiaohai.Input;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private InputReader _inputReader;

    [Header("Broadcasting on")]
    [SerializeField]
    private VoidEventChannel _startGameEventChannel;

    [SerializeField]
    private VoidEventChannel _playerResurrectEventChannel;

    [Header("Listening To")]
    [SerializeField]
    private VoidEventChannel _gameOverEventChannel;

    [SerializeField]
    private VoidEventChannel _restartGameEventChannel;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _startGameEventChannel.RaiseEvent();
    }

    void OnEnable()
    {
        _startGameEventChannel.OnEventRaised += HandleStartGame;
        _gameOverEventChannel.OnEventRaised += HandleGameOver;
        _restartGameEventChannel.OnEventRaised += HandleRestartGame;
    }

    void OnDisable()
    {
        _gameOverEventChannel.OnEventRaised -= HandleGameOver;
        _startGameEventChannel.OnEventRaised -= HandleStartGame;
        _restartGameEventChannel.OnEventRaised -= HandleRestartGame;
    }

    private void HandleStartGame()
    {
        _inputReader.Enable();
    }

    private void HandleGameOver()
    {
        _inputReader.Disable();
    }

    private void HandleRestartGame()
    {
        _playerResurrectEventChannel.RaiseEvent();
        _inputReader.Enable();
    }
}
