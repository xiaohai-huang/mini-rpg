using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(
    fileName = "Create Minion Event Channel",
    menuName = "My Scriptable Objects/Event Channel/Summoner's Rift/Create Minion Event Channel"
)]
public class CreateMinionEventChannel : ScriptableObject
{
    public event UnityAction<string, string, Transform> OnEventRaised;

    public void RaiseEvent(string id, string team, Transform spawnPoint)
    {
        OnEventRaised?.Invoke(id, team, spawnPoint);
    }
}
