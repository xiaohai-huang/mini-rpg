using System.Collections.Generic;
using System.Linq;
using Core.Game.Entities;
using UnityEngine;

namespace Core.Game.SpawnSystem
{
    public class MinionSpawnManager : MonoBehaviour
    {
        private MinionFactory _factory;

        [SerializeField]
        private MinionData _data;
        public HashSet<Base> SpawnEntities { get; private set; } = new HashSet<Base>();

        void Awake()
        {
            _factory = new MinionFactory(_data);
        }

        public Base Spawn(string id, string team, Transform spawnPoint)
        {
            var minion = _factory.Create(id, spawnPoint);
            SpawnEntities.Add(minion);
            minion.destroyCancellationToken.Register(() =>
            {
                SpawnEntities.Remove(minion);
            });
            // TODO: configure the team related stuff

            return minion;
        }

        public int ClearPuppets()
        {
            var puppets = SpawnEntities.Where(entity => entity.Id.StartsWith("puppet-"));
            foreach (var puppet in puppets)
            {
                Destroy(puppet.gameObject);
            }

            return SpawnEntities.RemoveWhere(entity => entity.Id.StartsWith("puppet-"));
        }
    }
}
