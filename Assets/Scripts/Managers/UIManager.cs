using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject GameOverScreen;

    public static UIManager Instance { get; private set; }
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



    public void ShowGameOver()
    {
        GameOverScreen.SetActive(true);
    }

    public void HideGameOver()
    {
        GameOverScreen.SetActive(false);
    }
}
