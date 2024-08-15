using UnityEngine;
using Xiaohai.Character;

namespace Core.Game.SpawnSystem
{
    public class HeroFactory : IEntityFactory<Character>
    {
        private readonly HerosData _data;

        public HeroFactory(HerosData data)
        {
            _data = data;
        }

        public Character Create(Transform spawnPoint)
        {
            throw new System.NotImplementedException();
        }

        public Character Create(int heroId, int skinId, Vector3 spawnPoint)
        {
            string id = $"{heroId}-{skinId}";
            var prefab = _data[id];
            var hero = Object.Instantiate(prefab, spawnPoint, Quaternion.identity);

            return hero;
        }
    }
}
