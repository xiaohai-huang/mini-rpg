using ReactUnity.Reactive;
using UnityEngine;

namespace Core.Game.UI
{
    public class ReactUnityBridge : MonoBehaviour
    {
        [Header("Broadcasting On")]
        [SerializeField]
        private StartGameEventChannel _startGameEventChannel;
        public ReactiveValue<string> Url = new("/");

        public void StartGame(int heroId, int skinId)
        {
            _startGameEventChannel.RaiseEvent(heroId, skinId);
        }

        public void Navigate(string newUrl)
        {
            Url.Value = newUrl;
        }
    }
}
