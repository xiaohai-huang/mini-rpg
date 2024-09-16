using System.Linq;
using Core.Game.Entities;
using Core.Game.Express;
using Core.Game.SpawnSystem;
using ReactUnity.Reactive;
using UnityEngine;

namespace Core.Game.UI
{
    public class ReactUnityBridge : MonoBehaviour
    {
        [SerializeField]
        private ReactUnity.ReactRendererBase _renderer;

        [SerializeField]
        private HeroSpawnManager _heroSpawnManager;

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
                    string id = req.body["id"].ToString();
                    var hero = _heroSpawnManager.SpawnEntities.First(entity => entity.Id == id);
                    hero.GetComponent<Level>().Upgrade(true);
                    res.end($"Successfully upgraded the player with id: {id}!");
                }
            );

            app.POST(
                "/reset-level",
                (req, res) =>
                {
                    string id = req.body["id"].ToString();
                    var hero = _heroSpawnManager.SpawnEntities.First(entity => entity.Id == id);
                    hero.GetComponent<Level>().Reset();
                    res.end($"Successfully reset the player's level to 0");
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
