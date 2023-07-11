using System;
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
    }

    void OnEnable()
    {
        _button.OnClick += HandleOnClick;
        _button.OnBeginInteraction += HandleOnBeginInteraction;
        _button.OnReleased += HandleOnRelease;
    }

    void OnDisable()
    {
        _button.OnClick -= HandleOnClick;
        _button.OnBeginInteraction -= HandleOnBeginInteraction;
        _button.OnReleased -= HandleOnRelease;
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

    private void HandleOnRelease(bool success)
    {
        if (_eventChannel != null)
            _eventChannel.RaiseReleaseEvent(_inputType);
    }
}