using System;
using UnityEngine;
using UnityEngine.AI;
using Xiaohai.Input;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    private CharacterController _characterController;
    private AttackHandler _attackHandler;

    NavMeshAgent navMeshAgent;
    private const float STOP_THRESHOLD = 0.1f;
    private Vector2 _input;
    public bool Moving
    {
        get
        {
            return navMeshAgent.remainingDistance > STOP_THRESHOLD;
        }
    }


    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        _characterController = GetComponent<CharacterController>();
        _attackHandler = GetComponent<AttackHandler>();
    }

    private void Update()
    {
        _input = _inputReader.InputActions.GamePlay.Move.ReadValue<Vector2>();
        if (_input == Vector2.zero) return;
        _attackHandler.AttackTarget = null;
        navMeshAgent.isStopped = true;
        Vector3 velocity = new Vector3(_input.x, 0, _input.y) * 3;
        _characterController.Move(velocity * Time.deltaTime);
    }



    public bool SetDestination(Vector3 target)
    {
        navMeshAgent.isStopped = false;
        return navMeshAgent.SetDestination(target);
    }

    internal void Stop()
    {
        navMeshAgent.isStopped = true;
    }
}
