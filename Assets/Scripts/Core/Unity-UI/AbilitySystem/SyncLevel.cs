using UnityEngine;
using UnityEngine.UI;

namespace Core.Game.UI
{
    public class SyncLevel : MonoBehaviour
    {
        private Material _mat;

        void Awake()
        {
            Image image = GetComponent<Image>();
            image.material = Instantiate(image.material);
            _mat = image.material;
        }

        public void HandleLevelChange(int current, int max)
        {
            _mat.SetFloat("_Level", current);
            _mat.SetFloat("_NumLevels", max);
        }
    }
}
