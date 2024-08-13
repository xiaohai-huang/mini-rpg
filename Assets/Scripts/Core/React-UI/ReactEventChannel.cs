using UnityEngine;

namespace Core.Game.UI
{
    public class ReactEventChannel : MonoBehaviour
    {
        [SerializeField]
        private StartGameEventChannel _startGameEventChannel;

        public void StartGame(int heroId, int skinId)
        {
            _startGameEventChannel.RaiseEvent(heroId, skinId);
        }
    }
}
