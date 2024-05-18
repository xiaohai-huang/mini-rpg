using UnityEngine;

namespace Core.Game
{
    public abstract class EntitySpawnManager : MonoBehaviour
    {
        [SerializeField]
        private SpawnPointStrategy _spawnPointStrategyType = SpawnPointStrategy.Linear;

        [SerializeField]
        protected Transform[] _spawnPoints;
        protected ISpawnPointStrategy _spawnPointStrategy;

        enum SpawnPointStrategy
        {
            Linear,
            Random
        }

        protected virtual void Awake()
        {
            switch (_spawnPointStrategyType)
            {
                case SpawnPointStrategy.Linear:
                {
                    _spawnPointStrategy = new LinearSpawnPointStrategy(_spawnPoints);
                    break;
                }
                case SpawnPointStrategy.Random:
                {
                    _spawnPointStrategy = new RandomSpawnPointStrategy(_spawnPoints);
                    break;
                }
            }
        }

        public abstract void Spawn();
    }
}
