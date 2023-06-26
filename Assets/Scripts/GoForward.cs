using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoForward : MonoBehaviour
{
    private float _timer;
    public float Speed;

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if(_timer > 10)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 velocity = transform.forward * Speed;
        transform.position += velocity * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"hit {other}");
        if(other.TryGetComponent(out HP hp))
        {
            hp.ReduceHP(10);
        }
        Destroy(gameObject);
    }
}
