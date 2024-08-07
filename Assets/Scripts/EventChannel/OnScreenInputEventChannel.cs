using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(
    fileName = "On Screen Input Event Channel",
    menuName = "My Scriptable Objects/Event Channel/On Screen Input Event Channel"
)]
public class OnScreenInputEventChannel : ScriptableObject
{
    public enum Input
    {
        BASIC_ATTACK,
        ABILITY_ONE,
        ABILITY_TWO,
        ABILITY_THREE,
        ABILITY_FOUR,
        HEAL
    }

    public UnityAction<Input> OnBeginInteractionEventRaised;
    public UnityAction<Input> OnMovingEventRaised;
    public UnityAction<Input> OnReleasedEventRaised;
    public UnityAction<Input, bool> OnCancellingChangedEventRaised;
    public UnityAction<Input, Vector2> OnClickEventRaised;
    private readonly Dictionary<Input, AbilityButton> _buttons = new();

    public void RaiseClickEvent(Input inputType, Vector2 position)
    {
        OnClickEventRaised?.Invoke(inputType, position);
    }

    public void RaiseBeginInteractionEvent(Input inputType)
    {
        OnBeginInteractionEventRaised?.Invoke(inputType);
    }

    public void RaiseOnMovingEvent(Input inputType)
    {
        OnMovingEventRaised?.Invoke(inputType);
    }

    public void RaiseReleaseEvent(Input inputType)
    {
        OnReleasedEventRaised?.Invoke(inputType);
    }

    public void RaiseCancellingChangedEvent(Input inputType, bool cancelling)
    {
        OnCancellingChangedEventRaised?.Invoke(inputType, cancelling);
    }

    public AbilityButton GetButton(Input inputType)
    {
        return _buttons[inputType];
    }

    public void AddButton(Input inputType, AbilityButton button)
    {
        _buttons.Add(inputType, button);
    }
}
