using UnityEngine;

namespace Core.Game
{
    public class CollectibleSpawnManager : EntitySpawnManager
    {
        [SerializeField]
        private CollectibleData _collectibleData;
        private EntitySpawner<Collectible> _spawner;
        private int _count;
        private int _timer;

        protected override void Awake()
        {
            base.Awake();
            _spawner = new EntitySpawner<Collectible>(
                new EntityFactory<Collectible>(_collectibleData),
                _spawnPointStrategy
            );

            _timer = Timer.Instance.SetInterval(
                () =>
                {
                    if (_count > _spawnPoints.Length)
                    {
                        Timer.Instance.ClearInterval(_timer);
                        Debug.Log("Stop Spawn");
                    }
                    Spawn();
                    _count++;
                },
                1000f
            );
        }

        public override void Spawn()
        {
            _spawner.Spawn();
        }
    }
}
