using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Game.UI
{
    public class AbilityCDVisual : MonoBehaviour
    {
        [SerializeField]
        private Image _blackBackground;

        [SerializeField]
        private TextMeshProUGUI _number;

        /// <summary>
        /// Measured in seconds
        /// </summary>
        public float Interval = 0.01f;

        void Awake()
        {
            Hide();
        }

        public void HandleCoolDownChange(float current, float total)
        {
            if (current <= 0)
            {
                Hide();
            }
            else
            {
                Show();
                _blackBackground.fillAmount = current / total;
                _number.text = current.ToString("F2");
            }
        }

        private void Show()
        {
            _blackBackground.gameObject.SetActive(true);
            _number.gameObject.SetActive(true);
        }

        public void Hide()
        {
            _blackBackground.gameObject.SetActive(false);
            _number.gameObject.SetActive(false);
        }
    }
}
