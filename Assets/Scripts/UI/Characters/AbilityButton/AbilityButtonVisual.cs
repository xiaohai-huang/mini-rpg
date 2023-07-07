using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AbilityButton))]
public class AbilityButtonVisual : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private Image _dot;
    [SerializeField] private Image _abilityIcon;
    [SerializeField] private Image _abilityLevel;

    [SerializeField] private Color _activeColor;
    [SerializeField] private Color _cancelColor;
    [Tooltip("The size of the button in percentage based on 100px button.")]
    [Range(0, 2f)]
    [SerializeField] private float _buttonSize = 1f;
    private const float BASE_SIZE = 100f;
    private AbilityButton _button;
    void Awake()
    {
        _button = GetComponent<AbilityButton>();
        _abilityIcon.rectTransform.sizeDelta = new Vector2(BASE_SIZE, BASE_SIZE) * _buttonSize;
        _abilityLevel.rectTransform.sizeDelta = new Vector2(BASE_SIZE + 15f, BASE_SIZE + 15f) * _buttonSize;
        _background.rectTransform.sizeDelta = new Vector2(_button.MovementRange * 2, _button.MovementRange * 2);
    }

    void OnEnable()
    {
        _button.OnBeginInteraction += OnBeginInteraction;
        _button.OnMoving += OnMoving;
        _button.OnReleased += OnReleased;
    }

    void OnDisable()
    {
        _button.OnBeginInteraction -= OnBeginInteraction;
        _button.OnMoving -= OnMoving;
        _button.OnReleased -= OnReleased;
    }

    void OnBeginInteraction()
    {
        _background.gameObject.SetActive(true);
        _dot.gameObject.SetActive(true);
        _dot.rectTransform.anchoredPosition = Vector2.zero;
        _background.color = _activeColor;
    }

    private void OnMoving()
    {
        _dot.rectTransform.anchoredPosition = _button.PointerPosition;
        if (_button.Cancelling)
        {
            _background.color = _cancelColor;
        }
        else
        {
            _background.color = _activeColor;
        }
    }

    private void OnReleased(bool arg0)
    {
        _background.gameObject.SetActive(false);
        _dot.gameObject.SetActive(false);
    }
#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        _abilityIcon.rectTransform.sizeDelta = new Vector2(BASE_SIZE, BASE_SIZE) * _buttonSize;
        _abilityLevel.rectTransform.sizeDelta = new Vector2(BASE_SIZE + 15f, BASE_SIZE + 15f) * _buttonSize;
    }
#endif
}
