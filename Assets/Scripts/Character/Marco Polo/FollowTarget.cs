using UnityEngine;
using Xiaohai.Character;

public class FollowTarget : MonoBehaviour
{
    public float Speed;
    public int DamageAmount;
    public Transform Target;
    public Vector3 Offset;
    void Start()
    {
        Destroy(gameObject, 10f);
    }
    // Update is called once per frame
    void Update()
    {
        if (Target != null)
        {
            transform.LookAt(Target);
            transform.position = Vector3.MoveTowards(transform.position, Target.position + Offset, Speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Damageable damageable))
        {
            damageable.TakeDamage(DamageAmount);
        }
        Destroy(gameObject);
    }
}
