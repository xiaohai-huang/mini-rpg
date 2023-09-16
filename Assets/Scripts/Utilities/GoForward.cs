using UnityEngine;
using Xiaohai.Character;

public class GoForward : MonoBehaviour
{
    public float Speed;
    public int DamageAmount = 10;
    void Start()
    {
        Destroy(gameObject, 10f);
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = transform.forward * Speed;
        transform.position += velocity * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Damageable damageable))
        {
            damageable.TakeDamage(DamageAmount);
            Destroy(gameObject);
        }
    }
}
