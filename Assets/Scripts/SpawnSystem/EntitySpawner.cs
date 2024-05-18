namespace Core.Game
{
    public class EntitySpawner<T>
        where T : Entity
    {
        readonly IEntityFactory<T> _entityFactory;
        readonly ISpawnPointStrategy _spawnPointStrategy;

        public EntitySpawner(
            IEntityFactory<T> entityFactory,
            ISpawnPointStrategy spawnPointStrategy
        )
        {
            _entityFactory = entityFactory;
            _spawnPointStrategy = spawnPointStrategy;
        }

        public T Spawn()
        {
            return _entityFactory.Create(_spawnPointStrategy.GetNextSpawnPoint());
        }
    }
}
