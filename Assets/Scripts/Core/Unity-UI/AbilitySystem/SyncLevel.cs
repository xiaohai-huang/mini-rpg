using UnityEngine;
using UnityEngine.UI;

namespace Core.Game.UI
{
    public class SyncLevel : MonoBehaviour
    {
        private Material _mat;

        public void HandleLevelChange(int current, int max)
        {
            if (_mat == null)
            {
                Image image = GetComponent<Image>();
                image.material = Instantiate(image.material);
                _mat = image.material;
            }
            _mat.SetFloat("_Level", current);
            _mat.SetFloat("_NumLevels", max);
        }
    }
}
