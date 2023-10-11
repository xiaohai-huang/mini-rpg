using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AbilityButton))]
public class IconButtonVisual : MonoBehaviour
{
    public bool ScaleAnimation = true;
    public ShowDotStrategies ShowDotStrategy = ShowDotStrategies.AlwaysShow;
    public enum ShowDotStrategies
    {
        Hidden = 0,
        AlwaysShow,
        ShowAfterMove
    }
    [SerializeField] private Image _background;
    [SerializeField] private Image _dot;
    [SerializeField] private RectTransform _container;
    [SerializeField] private Color _activeColor;
    [SerializeField] private Color _cancelColor;

    [Tooltip("The size of the button in percentage based on 100px button.")]
    [Range(0, 2f)]
    [SerializeField] private float _buttonSize = 1f;
    private const float BASE_SIZE = 100f;
    private AbilityButton _button;

    private float _scale = 1f;
    private const float SCALE_TRANSITION_SPEED = 8f;
    private const float PRESSED_SCALE = 0.8f;
    void Awake()
    {
        _button = GetComponent<AbilityButton>();
        _background.rectTransform.sizeDelta = new Vector2(_button.MovementRange * 2, _button.MovementRange * 2);
    }

    void Update()
    {
        if (ScaleAnimation)
        {
            _container.localScale = Vector3.Lerp(_container.localScale, new Vector3(_scale, _scale, 1f), SCALE_TRANSITION_SPEED * Time.deltaTime);
        }
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
        if (ShowDotStrategy == ShowDotStrategies.AlwaysShow)
        {
            _button.CancelButton.gameObject.SetActive(true);
            _background.gameObject.SetActive(true);
            _dot.gameObject.SetActive(true);
            _dot.rectTransform.anchoredPosition = Vector2.zero;
            _background.color = _activeColor;
        }
        else if (ShowDotStrategy == ShowDotStrategies.ShowAfterMove)
        {
            _dot.rectTransform.anchoredPosition = Vector2.zero;
            _background.color = _activeColor;
        }
        else if (ShowDotStrategy == ShowDotStrategies.Hidden)
        {
            _button.CancelButton.gameObject.SetActive(false);
            _background.gameObject.SetActive(false);
            _dot.gameObject.SetActive(false);
        }

        _scale = PRESSED_SCALE;
    }

    private void OnMoving()
    {
        if (ShowDotStrategy == ShowDotStrategies.ShowAfterMove)
        {
            _button.CancelButton.gameObject.SetActive(true);
            _background.gameObject.SetActive(true);
            _dot.gameObject.SetActive(true);
        }
        if (ShowDotStrategy == ShowDotStrategies.AlwaysShow || ShowDotStrategy == ShowDotStrategies.ShowAfterMove)
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
    }

    private void OnReleased(bool arg0)
    {
        if (ShowDotStrategy == ShowDotStrategies.AlwaysShow || ShowDotStrategy == ShowDotStrategies.ShowAfterMove)
        {
            _background.gameObject.SetActive(false);
            _dot.gameObject.SetActive(false);
            _button.CancelButton.gameObject.SetActive(false);
        }

        _scale = 1f;
    }

    void OnDrawGizmosSelected()
    {
        ((RectTransform)transform).sizeDelta = new Vector2(BASE_SIZE, BASE_SIZE) * _buttonSize;
    }
}
