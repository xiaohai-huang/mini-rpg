using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public float MovementRange = 150f;

    [SerializeField]
    private RectTransform _cancelButton;
    public RectTransform CancelButton => _cancelButton;

    /// <summary>
    /// The argument is the position of the release point. The values of each component is between 0 and 1.
    /// </summary>
    public UnityAction<Vector2> OnClick;
    public UnityAction OnBeginInteraction;
    public UnityAction OnMoving;
    public UnityAction<bool> OnReleased;
    public UnityAction<bool> OnCancellingChanged;

    /// <summary>
    /// Pointer position relative to the button position. It is restricted by <see cref="MovementRange" />, a clamped version of the <see cref="RealPointerPosition" />
    /// </summary>
    public Vector2 PointerPosition => _dot.rectTransform.anchoredPosition;

    /// <summary>
    /// Pointer position relative to the button position.
    /// </summary>
    public Vector2 RealPointerPosition { get; private set; }

    private bool _cancelling;
    public bool Cancelling
    {
        get { return _cancelling; }
        private set
        {
            if (value != _cancelling)
            {
                OnCancellingChanged?.Invoke(value);
            }
            _cancelling = value;
        }
    }
    public Vector2 Direction => PointerPosition.normalized;
    public Vector2 Position => PointerPosition / MovementRange;

    private Image _dot;

    private void Start()
    {
        _dot = new GameObject("Invisible Dot", typeof(Image)).GetComponent<Image>();
        _dot.transform.SetParent(transform);
        _dot.color = new Color(1, 1, 1, 0);
        _dot.raycastTarget = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _dot.rectTransform.anchoredPosition = Vector2.zero;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)transform,
            eventData.position,
            eventData.pressEventCamera,
            out var positionRelativeToOrigin
        );
        RealPointerPosition = positionRelativeToOrigin;

        OnBeginInteraction?.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)transform,
            eventData.position,
            eventData.pressEventCamera,
            out var positionRelativeToOrigin
        );
        var restrictedPosition = Vector2.ClampMagnitude(positionRelativeToOrigin, MovementRange);
        _dot.rectTransform.anchoredPosition = restrictedPosition;

        // check if the pointer is above over the cancel button
        if (RectTransformUtility.RectangleContainsScreenPoint(_cancelButton, eventData.position))
        {
            Cancelling = true;
        }
        else
        {
            Cancelling = false;
        }
        RealPointerPosition = positionRelativeToOrigin;
        OnMoving?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!RectTransformUtility.RectangleContainsScreenPoint(_cancelButton, eventData.position))
        {
            OnClick?.Invoke(Position);
            OnReleased?.Invoke(true);
        }
        else
        {
            OnReleased?.Invoke(false);
        }
        RealPointerPosition = Vector2.zero;
        Cancelling = false;
    }
}
