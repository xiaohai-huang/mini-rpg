using Core.Game.Express;
using ReactUnity.Reactive;
using UnityEngine;

namespace Core.Game.UI
{
    public class ReactUnityBridge : MonoBehaviour
    {
        [SerializeField]
        private ReactUnity.ReactRendererBase _renderer;

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
                    int id = req.body["id"].ToInt();
                    var nums = req.body["nums"].ToArray<float>();
                    Debug.Log("Increment the level by one for the player");
                    res.end($"Successfully upgraded the player with id: {id}!");
                }
            );
        }

        public void CreateRouter(object addRoute)
        {
            app = new App(_renderer.Context.Script.Engine);
            app.Init(addRoute);
            AddRoutes();
        }
    }
}
