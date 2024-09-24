using UnityEngine;

namespace Core.SummonersRift
{
    public class MinionWaveManager : MonoBehaviour
    {
        public bool MinionWave { get; private set; }

        [SerializeField]
        private Transform[] _minionSpawnPoints = new Transform[2];

        [Header("Broadcasting On")]
        [SerializeField]
        private CreateMinionEventChannel _createMinionEventChannel;

        [SerializeField]
        private VoidEventChannel _killMinionsEventChannel;

        private void Spawn(string id, string team, Transform spawnPoint)
        {
            _createMinionEventChannel.RaiseEvent(id, team, spawnPoint);
        }

        int _waveSpawnTimer;

        private void ActivateMinionWave()
        {
            if (MinionWave)
                return;
            _waveSpawnTimer = Timer.Instance.SetInterval(
                () =>
                {
                    Spawn("minion-01", "red", _minionSpawnPoints[1]);
                },
                10_000f,
                immediate: true
            );
            MinionWave = true;
        }

        private void DisableMinionWave()
        {
            Timer.Instance.ClearInterval(_waveSpawnTimer);
            MinionWave = false;
            _killMinionsEventChannel.RaiseEvent();
        }

        public void HandleMinionWaveRequest(int requestCode)
        {
            if (requestCode == 0)
            {
                DisableMinionWave();
            }
            else if (requestCode == 1)
            {
                ActivateMinionWave();
            }
        }
    }
}
