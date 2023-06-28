using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xiaohai.Input;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [Header("Broadcasting on")]
    public VoidEventChannel PlayerResurrectEventChannel;

    [Header("Listening on")]
    public VoidEventChannel PlayerDefeatEventChannel;
    public GameObject Player;
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
        Player = GameObject.FindGameObjectWithTag("Player");
        _inputReader.InputActions.Enable();
        PlayerDefeatEventChannel.OnEventRaised += OnPlayerDeath;
    }

    void Update()
    {
    }

    public void Restart()
    {
        UIManager.Instance.HideGameOver();
        PlayerResurrectEventChannel.RaiseEvent();
    }

    private void OnPlayerDeath()
    {
        Debug.Log("Game is over");
        UIManager.Instance.ShowGameOver();
    }
}
