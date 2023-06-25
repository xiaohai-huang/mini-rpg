using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xiaohai.Input;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    // Start is called before the first frame update
    void Start()
    {
        _inputReader.InputActions.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
