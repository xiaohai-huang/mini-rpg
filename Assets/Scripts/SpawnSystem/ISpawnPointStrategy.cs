using UnityEngine;

namespace Core.Game
{
    public interface ISpawnPointStrategy
    {
        Transform GetNextSpawnPoint();
    }
}
