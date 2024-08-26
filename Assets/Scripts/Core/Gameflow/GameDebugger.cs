using UnityEngine;
using Xiaohai.Character;

namespace Core.Game.Flow
{
    public class GameDebugger : MonoBehaviour
    {
        [SerializeField]
        private StartGameEventChannel _startGameEventChannel;
        public int HeroId;
        public int SkinId;
        public Character _character;
        public bool UseExistingCharacter;

        void Start()
        {
            StartGame();
        }

        void StartGame()
        {
            if (UseExistingCharacter)
            {
                FindObjectsByType<BasicGameFlow>(FindObjectsSortMode.InstanceID)[0]
                    .InitGame(_character);
                Timer.Instance.SetTimeout(
                    () =>
                    {
                        _character.Level.Upgrade();
                        _character.Level.Upgrade();
                        _character.Level.Upgrade();
                    },
                    500f
                );
            }
            else
            {
                _startGameEventChannel.RaiseEvent(HeroId, SkinId);
            }
        }
    }
}
