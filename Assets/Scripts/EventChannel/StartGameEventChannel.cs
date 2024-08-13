using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(
    fileName = "Start Game Event Channel",
    menuName = "My Scriptable Objects/Event Channel/Start Game Event Channel"
)]
public class StartGameEventChannel : ScriptableObject
{
    public UnityAction<int, int> OnEventRaised;

    public void RaiseEvent(int heroId, int skinId)
    {
        OnEventRaised?.Invoke(heroId, skinId);
    }
}
