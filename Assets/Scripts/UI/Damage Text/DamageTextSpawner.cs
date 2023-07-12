using UnityEngine;

public class DamageTextSpawner : MonoBehaviour
{
    [SerializeField] private DamageText _damageTextPrefab;
    [SerializeField] private Transform _left;
    [SerializeField] private Transform _right;

    public void HandleDamage(int damageAmount)
    {
        bool showLeft = Random.Range(0, 2) == 0;
        if (showLeft)
        {
            var text = Instantiate(_damageTextPrefab, _left);
            text.gameObject.SetActive(true);
            text.ShowLeft(damageAmount);
        }
        else
        {
            var text = Instantiate(_damageTextPrefab, _right);
            text.gameObject.SetActive(true);
            text.ShowRight(damageAmount);
        }
    }
}
