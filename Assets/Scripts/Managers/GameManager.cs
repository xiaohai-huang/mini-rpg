using UnityEngine;
using Xiaohai.Input;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [Header("Broadcasting on")]
    [SerializeField] private VoidEventChannel StartGameEventChannel;



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
        StartGameEventChannel.RaiseEvent();
    }




}
