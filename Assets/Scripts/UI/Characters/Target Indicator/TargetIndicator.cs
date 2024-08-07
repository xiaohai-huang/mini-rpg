using UnityEngine;
using Xiaohai.Character;

namespace Xiaohai.UI
{
    public class TargetIndicator : MonoBehaviour
    {
        [SerializeField]
        private RuntimeCharacterAnchor _player;

        [SerializeField]
        private GameObject _circle;

        [SerializeField]
        private Damageable _damageable;

        // Update is called once per frame
        void Update()
        {
            if (_player.Value.Target == _damageable)
            {
                _circle.SetActive(true);
            }
            else
            {
                _circle.SetActive(false);
            }
        }
    }
}
