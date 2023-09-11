using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xiaohai.Character;

public class StaticEnemy : MonoBehaviour
{
    private Damageable _damageable;
    void Awake()
    {
        _damageable = GetComponent<Damageable>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _damageable.OnDie.AddListener(Damageable_OnDie);
    }

    void Damageable_OnDie()
    {
        Timer.Instance.SetTimeout(() =>
        {
            _damageable.Resurrect();
        }, 500f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
