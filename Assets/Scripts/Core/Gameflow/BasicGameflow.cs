using Core.Game.SpawnSystem;
using UnityEngine;
using UnityHFSM;

namespace Core.Game.Flow
{
    public class BasicGameFlow : MonoBehaviour
    {
        private StateMachine _fsm;

        [SerializeField]
        private HeroSpawnManager _heroSpawnManager;

        /// <summary>
        /// Total play time.
        /// </summary>
        private float _timer;
        private int _heroId;
        private int _skinId;

        void Start()
        {
            _fsm = new StateMachine();
            _fsm.AddState("Start");
            _fsm.AddState(
                "Initialization",
                onEnter: (_) =>
                {
                    // Create the chosen hero for the player
                    _heroSpawnManager.SpawnPlayer(_heroId, _skinId, Vector3.zero);
                }
            );

            _fsm.AddState(
                "Playing",
                onLogic: (_) =>
                {
                    _timer += Time.deltaTime;
                }
            );

            _fsm.AddState("Paused");

            _fsm.AddState(
                "GameOver",
                onEnter: (_) => {
                    // Play crystal blow up animation
                },
                onExit: (_) => {
                    // Navigate to Post game summary page
                }
            );

            _fsm.AddTriggerTransition("StartInit", new Transition("Start", "Initialization"));
            _fsm.SetStartState("Start");
            _fsm.Init();
        }

        void Update()
        {
            _fsm.OnLogic();
        }

        public void InitGame(int heroId, int skinId)
        {
            _heroId = heroId;
            _skinId = skinId;
            _fsm.Trigger("StartInit");
        }
    }
}
