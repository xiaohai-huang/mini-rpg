using UnityEngine;

namespace Core.Game.Flow
{
    public class GameDebugger : MonoBehaviour
    {
        [SerializeField]
        private StartGameEventChannel _startGameEventChannel;
        public int HeroId;
        public int SkinId;

        void Start()
        {
            StartGame();
        }

        void StartGame()
        {
            _startGameEventChannel.RaiseEvent(HeroId, SkinId);
        }
    }
}
