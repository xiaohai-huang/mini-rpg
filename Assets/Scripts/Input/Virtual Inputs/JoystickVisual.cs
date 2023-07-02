using UnityEngine;
using UnityEngine.UI;

namespace Xiaohai.Input
{
    public class JoystickVisual : MonoBehaviour
    {
        [SerializeField] private InputReader _inputReader;
        [SerializeField] private Image _backgroundRing;
        [SerializeField] private Image _dot;
        public float Width = 300f;
        public float Height = 300f;
        private OnScreenJoystick _joystick;
        private Vector2 _initialSize;
        private Vector2 _halfSize;

        void Start()
        {
            _joystick = GetComponent<OnScreenJoystick>();

            // Calculate the initial size and half size of the visual
            _initialSize = new Vector2(Width, Height);
            _halfSize = _initialSize / 2f;

            // Update the size of the visual
            _backgroundRing.rectTransform.sizeDelta = _initialSize;
            _dot.rectTransform.sizeDelta = _halfSize;
        }

        void Update()
        {
            // Update the position of the backgroundRing based on the joystick's PointerDownPosition
            _backgroundRing.rectTransform.anchoredPosition = _joystick.PointerDownPosition;

            // Calculate the x and y values based on the input reader's Move values and half size of the visual
            float x = _inputReader.Move.x * _halfSize.x;
            float y = _inputReader.Move.y * _halfSize.y;

            // Create an offset vector based on the calculated x and y values
            Vector2 offset = new Vector2(x, y);

            // Update the position of the dot based on the joystick's PointerDownPosition and the offset
            _dot.rectTransform.anchoredPosition = _joystick.PointerDownPosition + offset;
        }
    }
}
