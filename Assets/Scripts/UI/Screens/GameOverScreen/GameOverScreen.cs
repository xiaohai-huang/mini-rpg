using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameOverUI;

    public void Show()
    {
        _gameOverUI.SetActive(true);
    }

    public void Hide()
    {
        _gameOverUI.SetActive(false);
    }
}
