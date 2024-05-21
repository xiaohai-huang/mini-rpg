using System.Collections.Generic;
using UnityEngine;

namespace Core.Game
{
    public class RandomSpawnPointStrategy : ISpawnPointStrategy
    {
        private readonly Transform[] _points;
        private List<Transform> _unusedPoints;

        public RandomSpawnPointStrategy(Transform[] points)
        {
            _points = points;
            _unusedPoints = new List<Transform>(points);
        }

        public Transform GetNextSpawnPoint()
        {
            if (_unusedPoints.Count == 0)
            {
                _unusedPoints = new List<Transform>(_points);
            }

            int index = Random.Range(0, _unusedPoints.Count);
            var point = _unusedPoints[index];
            _unusedPoints.RemoveAt(index);
            return point;
        }
    }
}
