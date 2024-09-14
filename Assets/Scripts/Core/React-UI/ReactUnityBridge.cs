using Core.Game.Express;
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

        public App app;

        public void AddRoutes()
        {
            app.GET(
                "/",
                (req, res) =>
                {
                    res.end("Hello mate from C#. The index route");
                }
            );

            app.POST(
                "/increment-level",
                (req, res) =>
                {
                    string body = req.body;
                    Debug.Log(body);
                    Debug.Log("Increment the level for player");
                    res.end("");
                }
            );
        }

        public void CreateRouter(object addRoute)
        {
            app = new App();
            app.Init(addRoute);
            AddRoutes();
        }
    }
}
