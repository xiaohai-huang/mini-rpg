using Core.Game.SpawnSystem;
using UnityEngine;
using UnityHFSM;
using Xiaohai.Input;

namespace Core.Game.Flow
{
    public class BasicGameFlow : MonoBehaviour
    {
        private StateMachine _fsm;

        [SerializeField]
        private InputReader _inputReader;

        [SerializeField]
        private HeroSpawnManager _heroSpawnManager;

        /// <summary>
        /// Total play time.
        /// </summary>
        private float _timer;
        private int _heroId;
        private int _skinId;

        private bool _ready;

        void Start()
        {
            _fsm = new StateMachine();
            _fsm.AddState(
                "Initialization",
                onEnter: (_) =>
                {
                    // Create the chosen hero for the player
                    _heroSpawnManager.SpawnPlayer(_heroId, _skinId, Vector3.zero);
                    _inputReader.Enable();
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
                onEnter: (_) =>
                {
                    // Play crystal blow up animation
                    _inputReader.Disable();
                },
                onExit: (_) => {
                    // Navigate to Post game summary page
                }
            );
            _fsm.AddState("Ghost");
            _fsm.AddTransition(new Transition("Ghost", "Initialization", (_) => _ready));
            _fsm.SetStartState("Ghost");
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

            _ready = true;
        }
    }
}
