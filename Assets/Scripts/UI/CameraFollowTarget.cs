using Core.Game.Common;
using UnityEngine;

namespace Core.Game.UI
{
    public class CameraFollowTarget : MonoBehaviour
    {
        [SerializeField]
        private RuntimeTransformAnchor _anchor;

        [SerializeField]
        private RuntimeCharacterAnchor _player;

        void Awake()
        {
            _anchor.Provide(transform);
        }

        public void FollowPlayer()
        {
            transform.SetParent(_player.Value.transform);
            transform.localPosition = Vector3.zero;
        }

        public void MoveOutOfPlayerHierarchy()
        {
            transform.SetParent(null);
        }

        /// <summary>
        /// Move the current transform with the given position.
        /// </summary>
        /// <param name="position">The center of the map is (0,0). The left bottom is (-1, -1)</param>
        public void MoveWith(Vector2 position)
        {
            var mapSize = Constants.MAP_SIZE;
            transform.position = new Vector3(position.x * mapSize.x, 0, position.y * mapSize.y);
        }
    }
}
