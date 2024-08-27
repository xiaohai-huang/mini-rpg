using UnityEngine;
using UnityEngine.UI;

namespace Core.Game.UI
{
    public class AbilityDarkCover : MonoBehaviour
    {
        private Image _image;
        private bool _hasEnoughLevel;

        void Awake()
        {
            _image = GetComponent<Image>();
        }

        public void HandleLevelChange(int current, int _)
        {
            _hasEnoughLevel = current > 0;
            Sync();
        }

        void Sync()
        {
            bool show = !_hasEnoughLevel;
            _image.enabled = show;
        }
    }
}
