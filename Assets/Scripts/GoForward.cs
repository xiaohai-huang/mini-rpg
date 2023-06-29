using UnityEngine;
using Xiaohai.Character;

public class GoForward : MonoBehaviour
{
    private float _timer;
    public float Speed;

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > 10)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 velocity = transform.forward * Speed;
        transform.position += velocity * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Damageable damageable))
        {
            damageable.TakeDamage(10);
        }
        Destroy(gameObject);
    }
}
