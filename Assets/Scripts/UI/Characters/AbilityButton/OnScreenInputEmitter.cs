using UnityEngine;

[RequireComponent(typeof(AbilityButton))]
public class OnScreenInputEmitter : MonoBehaviour
{
    [SerializeField] private OnScreenInputEventChannel _eventChannel;
    [SerializeField] private OnScreenInputEventChannel.Input _inputType;
    private AbilityButton _button;
    void Awake()
    {
        _button = GetComponent<AbilityButton>();
        _eventChannel.AddButton(_inputType, _button);
    }

    void OnEnable()
    {
        _button.OnBeginInteraction += HandleOnBeginInteraction;
        _button.OnMoving += HandleOnMoving;
        _button.OnReleased += HandleOnRelease;
        _button.OnCancellingChanged += HandleOnCancellingChanged;
        _button.OnClick += HandleOnClick;
    }

    void OnDisable()
    {
        _button.OnClick -= HandleOnClick;
        _button.OnBeginInteraction -= HandleOnBeginInteraction;
        _button.OnMoving -= HandleOnMoving;
        _button.OnReleased -= HandleOnRelease;
        _button.OnCancellingChanged -= HandleOnCancellingChanged;
    }

    private void HandleOnClick(Vector2 position)
    {
        if (_eventChannel != null)
            _eventChannel.RaiseClickEvent(_inputType, position);
    }

    private void HandleOnBeginInteraction()
    {
        if (_eventChannel != null)
            _eventChannel.RaiseBeginInteractionEvent(_inputType);
    }

    private void HandleOnMoving()
    {
        if (_eventChannel != null)
            _eventChannel.RaiseOnMovingEvent(_inputType);
    }

    private void HandleOnRelease(bool success)
    {
        if (_eventChannel != null)
            _eventChannel.RaiseReleaseEvent(_inputType);
    }

    private void HandleOnCancellingChanged(bool cancelling)
    {
        if (_eventChannel != null)
            _eventChannel.RaiseCancellingChangedEvent(_inputType, cancelling);
    }
}