using UnityEngine;

public class MarcoPolo : MonoBehaviour
{
    public Transform LeftGunFirePoint;
    public Transform RightGunFirePoint;
    public GameObject BulletPrefab;
    private bool _attackHand;
    private Character _character;
    void Awake()
    {
        _character = GetComponent<Character>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Attack()
    {
        if (_attackHand)
        {
            FireBullet(LeftGunFirePoint);
        }
        else
        {
            FireBullet(RightGunFirePoint);
        }
        _attackHand = !_attackHand;
    }

    void ConsumeAttackInput()
    {
        _character.AttackInput = false;
    }

    GameObject FireBullet(Transform firePoint)
    {
        var bullet = Instantiate(BulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<GoForward>().Speed = 10f;
        return bullet;
    }

    void FireBullet()
    {
        if (_attackHand)
        {
            FireBullet(LeftGunFirePoint);
        }
        else
        {
            FireBullet(RightGunFirePoint);
        }
        _attackHand = !_attackHand;
    }

    public void AbilityOne(int numBullets)
    {
        for (int i = 0; i < numBullets; i++)
        {
            FireBullet();
        }
    }
}
