using Core.Game.Entities;
using UnityEngine;

namespace Core.Game.SpawnSystem
{
    public class MinionFactory : IEntityFactory<Base>
    {
        private readonly MinionData _data;
        private static int _counter = 0;

        public MinionFactory(MinionData data)
        {
            _data = data;
        }

        public Base Create(Transform spawnPoint)
        {
            throw new System.NotImplementedException();
        }

        public Base Create(string id, Transform spawnPoint)
        {
            var prefab = _data[id];
            var minion = Object.Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
            minion.Id = $"{id}:{_counter}";
            _counter++;
            return minion;
        }
    }
}
