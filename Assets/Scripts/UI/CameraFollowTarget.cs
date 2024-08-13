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
        public float Smoothness;

        void Awake()
        {
            _anchor.Provide(transform);
        }

        public void FollowPlayer()
        {
            transform.SetParent(_player.Value.transform);
            transform.localPosition = Vector3.zero;
            _movedByPlayerPosition = true;
        }

        private bool _movedByPlayerPosition;

        public void MoveOutOfPlayerHierarchy()
        {
            transform.SetParent(null);
            _movedByPlayerPosition = false;
        }

        Vector3 _viewBoxPosition;

        /// <summary>
        /// Sync the position with view box position
        /// </summary>
        /// <param name="position">The center of the map is (0,0). The left bottom is (-1, -1)</param>
        public void UpdateViewBoxPosition(Vector2 position)
        {
            var mapSize = Constants.MAP_SIZE;
            _viewBoxPosition = new Vector3(position.x * mapSize.x, 0, position.y * mapSize.y);
        }

        void LateUpdate()
        {
            if (!_movedByPlayerPosition)
            {
                transform.position = Vector3.Lerp(
                    transform.position,
                    _viewBoxPosition,
                    Time.deltaTime * Smoothness
                );
            }
        }
    }
}
