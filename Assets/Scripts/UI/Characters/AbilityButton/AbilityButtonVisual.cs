using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AbilityButton))]
public class AbilityButtonVisual : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private Image _dot;
    [SerializeField] private Color _activeColor;
    [SerializeField] private Color _cancelColor;
    private AbilityButton _button;
    void Awake()
    {
        _button = GetComponent<AbilityButton>();
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
        if (_button.IsOverCancel)
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
}
