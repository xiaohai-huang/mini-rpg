using UnityEngine;

namespace Core.Game.UI
{
    public class SetUrl : MonoBehaviour
    {
        [Header("Broadcasting On")]
        [SerializeField]
        private StringEventChannel _navigateEventChannel;
        public string URL = "/";

        void Awake()
        {
            _navigateEventChannel.RaiseEvent(URL);
        }
    }
}
