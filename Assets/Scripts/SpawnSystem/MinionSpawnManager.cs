using System;
using System.Collections.Generic;
using System.Linq;
using Core.Game.Entities;
using UnityEngine;
using Xiaohai.Character;

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

        public void VoidSpawn(string id, string team, Transform spawnPoint)
        {
            Spawn(id, team, spawnPoint);
        }

        int ClearEntity(Func<Base, bool> filter)
        {
            var entity = SpawnEntities.Where(filter);
            foreach (var puppet in entity)
            {
                Destroy(puppet.gameObject);
            }

            return SpawnEntities.RemoveWhere(entity => filter(entity));
        }

        public int ClearPuppets()
        {
            return ClearEntity(entity => entity.Id.StartsWith("puppet-"));
        }

        public void KillMinions()
        {
            var minions = SpawnEntities.Where(entity => entity.Id.StartsWith("minion-"));
            foreach (var minion in minions)
            {
                minion.GetComponent<Damageable>().Kill();
            }
        }
    }
}
