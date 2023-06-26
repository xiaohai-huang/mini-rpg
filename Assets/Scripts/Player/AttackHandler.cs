using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    public Transform FirePoint;
    public GameObject BulletPrefab;
   

    internal void Attack()
    {
        var go = Instantiate(BulletPrefab);
        go.GetComponent<GoForward>().Speed = 10f;
        go.transform.SetPositionAndRotation(FirePoint.transform.position, FirePoint.transform.rotation);
    }
}
