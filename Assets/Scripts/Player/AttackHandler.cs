using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    public Transform FirePoint;
    public GameObject BulletPrefab;
    public int MaxDamageAmount = 200;

    internal void Attack()
    {
        var go = Instantiate(BulletPrefab);
        var goForward = go.GetComponent<GoForward>();
        goForward.Speed = 10f;
        goForward.DamageAmount = Random.Range(100, MaxDamageAmount);
        go.transform.SetPositionAndRotation(
            FirePoint.transform.position,
            FirePoint.transform.rotation
        );
    }
}
