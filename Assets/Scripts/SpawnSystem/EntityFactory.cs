using UnityEngine;

namespace Core.Game
{
    public class EntityFactory<T> : IEntityFactory<T>
        where T : Entity
    {
        private EntityData _entityData;

        public EntityFactory(EntityData entityData)
        {
            _entityData = entityData;
        }

        public T Create(Transform spawnPoint)
        {
            var go = GameObject.Instantiate(
                _entityData.Prefab,
                spawnPoint.position,
                spawnPoint.rotation
            );
            return go.GetComponent<T>();
        }
    }
}
