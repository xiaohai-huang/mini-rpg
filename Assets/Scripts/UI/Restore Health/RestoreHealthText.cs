using TMPro;
using UnityEngine;

public class RestoreHealthText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _text;

    // Start is called before the first frame update
    public void SetHealth(int health)
    {
        _text.text = $"+{health}";
    }

    private void OnEnable()
    {
        Destroy(gameObject, 2);
    }
}
