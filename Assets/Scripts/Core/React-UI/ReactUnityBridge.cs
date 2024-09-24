using System;
using System.Linq;
using Core.Game.Entities;
using Core.Game.Express;
using Core.Game.Mana;
using Core.Game.SpawnSystem;
using ReactUnity.Reactive;
using UnityEngine;
using Xiaohai.Character;

namespace Core.Game.UI
{
    public class ReactUnityBridge : MonoBehaviour
    {
        [SerializeField]
        private ReactUnity.ReactRendererBase _renderer;

        [SerializeField]
        private HeroSpawnManager _heroSpawnManager;

        [SerializeField]
        private MinionSpawnManager _minionSpawnManager;

        [SerializeField]
        private RuntimeCharacterAnchor _player;

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

            app.POST(
                "/spawn-puppet",
                (req, res) =>
                {
                    string id = req.body["id"].ToString();
                    string team = req.body["team"].ToString();
                    _minionSpawnManager.Spawn(id, team, _player.Value.transform);
                    res.end("Successfully spawn a puppet.");
                }
            );

            app.POST(
                "/clear-puppets",
                (req, res) =>
                {
                    try
                    {
                        int count = _minionSpawnManager.ClearPuppets();
                        res.end($"Successfully clear all puppets. {count} puppets were removed.");
                    }
                    catch (Exception e)
                    {
                        res.end($"Failed to clear puppets. {e.Message}");
                    }
                }
            );

            app.POST(
                "/set-zero-cooldown",
                (req, res) =>
                {
                    string id = req.body["id"].ToString();
                    bool enabled = req.body["enabled"].ToBoolean();
                    var text = enabled ? "zero" : "normal";
                    var hero = _heroSpawnManager.SpawnEntities.First(entity => entity.Id == id);
                    hero.GetComponent<ManaSystem>().ZeroCooldown = enabled;
                    res.end($"Hero (id:{id}) cooldown is set to {text}");
                }
            );

            app.POST(
                "/set-invincible",
                (req, res) =>
                {
                    string id = req.body["id"].ToString();
                    bool enabled = req.body["enabled"].ToBoolean();
                    var text = enabled ? "enabled" : "disabled";
                    var hero = _heroSpawnManager.SpawnEntities.First(entity => entity.Id == id);
                    hero.GetComponent<Damageable>().Invincible = enabled;
                    res.end($"Hero (id:{id}) invincible is set to {text}");
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
