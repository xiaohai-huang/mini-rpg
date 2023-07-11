using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "On Screen Input Event Channel", menuName = "My Scriptable Objects/Event Channel/On Screen Input Event Channel")]
public class OnScreenInputEventChannel : ScriptableObject
{
    public enum Input
    {
        ABILITY_ONE,
        ABILITY_TWO,
        ABILITY_THREE,
        ABILITY_FOUR,
        HEAL
    }
    public UnityAction<Input> OnBeginInteractionEventRaised;
    public UnityAction<Input> OnReleasedEventRaised;
    public UnityAction<Input, Vector2> OnClickEventRaised;

    public void RaiseClickEvent(Input inputType, Vector2 position)
    {
        OnClickEventRaised?.Invoke(inputType, position);
    }

    public void RaiseBeginInteractionEvent(Input inputType)
    {
        OnBeginInteractionEventRaised?.Invoke(inputType);
    }

    public void RaiseReleaseEvent(Input inputType)
    {
        OnReleasedEventRaised?.Invoke(inputType);
    }
}
