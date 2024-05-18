using UnityEngine;

namespace Core.Game
{
    public class LinearSpawnPointStrategy : ISpawnPointStrategy
    {
        private readonly Transform[] _points;
        private int _index;

        public LinearSpawnPointStrategy(Transform[] points)
        {
            _points = points;
        }

        public Transform GetNextSpawnPoint()
        {
            var point = _points[_index];
            _index = (_index + 1) % _points.Length;
            return point;
        }
    }
}
