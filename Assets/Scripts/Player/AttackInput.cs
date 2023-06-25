using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackInput : MonoBehaviour
{
    InteractInput _interactInput;
    AttackHandler _attackHandler;

    private void Awake()
    {
        _interactInput = GetComponent<InteractInput>();
        _attackHandler = GetComponent<AttackHandler>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(_interactInput.HoverringObject)
            {
                _attackHandler.Attack(_interactInput.HoverringObject);
            }
        }
    }
}
