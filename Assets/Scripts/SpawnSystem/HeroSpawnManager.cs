using Core.Game.Common;
using UnityEngine;
using UnityEngine.SceneManagement;
using Xiaohai.Character;

namespace Core.Game.SpawnSystem
{
    public class HeroSpawnManager : MonoBehaviour
    {
        private HeroFactory _factory;

        [SerializeField]
        private HerosData _data;

        [Header("Player")]
        [Header("Broadcasting On")]
        [SerializeField]
        private TransformEventChannel _playerSpawnedEventChannel;

        [SerializeField]
        private RuntimeCharacterAnchor _playerAnchor;

        void Awake()
        {
            _factory = new HeroFactory(_data);
        }

        public Character Spawn(int heroId, int skinId, Vector3 spawnPoint, string heroTag)
        {
            var hero = _factory.Create(heroId, skinId, spawnPoint);
            hero.Id = heroTag;
            return hero;
        }

        public Character SpawnPlayer(int heroId, int skinId, Vector3 spawnPoint)
        {
            var player = Spawn(heroId, skinId, spawnPoint, Constants.PLAYER_HERO_ID);

            player.gameObject.SetActive(true);
            _playerAnchor.Provide(player);
            _playerSpawnedEventChannel.RaiseEvent(player.transform);
            return player;
        }

        public Character SpawnPlayer(Character existingPlayer, Vector3 spawnPoint)
        {
            existingPlayer.transform.position = spawnPoint;
            existingPlayer.gameObject.SetActive(true);
            _playerAnchor.Provide(existingPlayer);
            _playerSpawnedEventChannel.RaiseEvent(existingPlayer.transform);
            return existingPlayer;
        }
    }
}
