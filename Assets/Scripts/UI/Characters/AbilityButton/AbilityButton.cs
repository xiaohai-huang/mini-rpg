using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public float MovementRange = 150f;
    [SerializeField] private RectTransform _cancelButton;

    public UnityAction<Vector2> OnClick;
    public UnityAction OnBeginInteraction;
    public UnityAction OnMoving;
    public UnityAction<bool> OnReleased;

    /// <summary>
    /// Pointer position relative to the button position. It is restricted by <see cref="MovementRange" />, a clamped version of the <see cref="RealPointerPosition" />
    /// </summary>
    public Vector2 PointerPosition => _dot.rectTransform.anchoredPosition;
    /// <summary>
    /// Pointer position relative to the button position.
    /// </summary>
    public Vector2 RealPointerPosition { get; private set; }
    public bool Cancelling { get; private set; }
    public Vector2 Direction => PointerPosition.normalized;
    public Vector2 Position => PointerPosition / MovementRange;

    private Image _dot;
    private void Start()
    {
        _dot = new GameObject("Invisible Dot", typeof(Image)).GetComponent<Image>();
        _dot.transform.SetParent(transform);
        _dot.color = new Color(1, 1, 1, 0);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _dot.rectTransform.anchoredPosition = Vector2.zero;
        _cancelButton.gameObject.SetActive(true);

        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)transform, eventData.position, eventData.pressEventCamera, out var positionRelativeToOrigin);
        RealPointerPosition = positionRelativeToOrigin;

        OnBeginInteraction?.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)transform, eventData.position, eventData.pressEventCamera, out var positionRelativeToOrigin);
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
            Cancelling = false;
            OnClick?.Invoke(_dot.rectTransform.anchoredPosition.normalized);
            OnReleased?.Invoke(true);
        }
        else
        {
            Cancelling = true;
            OnReleased?.Invoke(false);
        }
        _cancelButton.gameObject.SetActive(false);
        RealPointerPosition = Vector2.zero;
    }
}
