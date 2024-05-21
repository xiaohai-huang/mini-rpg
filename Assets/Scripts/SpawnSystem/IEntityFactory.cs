using UnityEngine;

namespace Core.Game
{
    public interface IEntityFactory<T>
        where T : Entity
    {
        T Create(Transform spawnPoint);
    }
}
