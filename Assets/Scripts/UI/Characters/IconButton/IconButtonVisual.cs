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

    [SerializeField]
    private Image _background;

    [SerializeField]
    protected GameObject _darkCover;

    [SerializeField]
    protected GameObject _purpleCover;

    [SerializeField]
    private Image _dot;

    [SerializeField]
    private RectTransform _container;

    [SerializeField]
    protected Color _activeColor;

    [SerializeField]
    protected Color _cancelColor;

    [Tooltip("The size of the button in percentage based on 100px button.")]
    [Range(0, 2f)]
    [SerializeField]
    private float _buttonSize = 1f;
    private const float BASE_SIZE = 100f;
    protected AbilityButton _button;

    private float _scale = 1f;
    private const float SCALE_TRANSITION_SPEED = 8f;
    private const float PRESSED_SCALE = 0.8f;

    public virtual void Awake()
    {
        _button = GetComponent<AbilityButton>();
        _background.rectTransform.sizeDelta = new Vector2(
            _button.MovementRange * 2,
            _button.MovementRange * 2
        );
    }

    public virtual void Update()
    {
        if (ScaleAnimation)
        {
            _container.localScale = Vector3.Lerp(
                _container.localScale,
                new Vector3(_scale, _scale, 1f),
                SCALE_TRANSITION_SPEED * Time.deltaTime
            );
        }

        _background.color = GetBackgroundColor();
    }

    protected virtual Color GetBackgroundColor()
    {
        return _button.Cancelling ? _cancelColor : _activeColor;
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
        if (
            ShowDotStrategy == ShowDotStrategies.AlwaysShow
            || ShowDotStrategy == ShowDotStrategies.ShowAfterMove
        )
        {
            _dot.rectTransform.anchoredPosition = _button.PointerPosition;
        }
    }

    private void OnReleased(bool arg0)
    {
        if (
            ShowDotStrategy == ShowDotStrategies.AlwaysShow
            || ShowDotStrategy == ShowDotStrategies.ShowAfterMove
        )
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
