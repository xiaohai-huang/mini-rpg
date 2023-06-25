using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    [SerializeField] float _attackRange = 1;
    CharacterMovement _characterMovement;
    public Interactable AttackTarget;
    private void Awake()
    {
        _characterMovement = GetComponent<CharacterMovement>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (AttackTarget)
        {
            if (Vector3.Distance(transform.position, AttackTarget.transform.position) < _attackRange)
            {
                _characterMovement.Stop();
                Debug.Log("Attack " + AttackTarget.name);
                AttackTarget = null;
            }
            else
            {
                _characterMovement.SetDestination(AttackTarget.transform.position);
            }
        }
    }

    internal void Attack(Interactable target)
    {
        AttackTarget = target;
    }
}
