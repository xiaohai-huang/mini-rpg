using UnityEngine;

namespace Core.Game.UI
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField]
        private RuntimeTransformAnchor _target;

        [SerializeField]
        private RuntimeTransformAnchor _player;

        public void FollowPlayer()
        {
            _target.Value.SetParent(_player.Value);
            _target.Value.transform.position = Vector3.zero;
        }

        public void OnViewBoxMove() { }
    }
}
