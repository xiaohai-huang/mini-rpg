using UnityEngine;
using UnityEngine.UI;

namespace Xiaohai.Input
{
    public class JoystickVisual : MonoBehaviour
    {
        [SerializeField]
        private Image _backgroundRing;

        [SerializeField]
        private Image _dot;
        public float Size = 300f;
        private float _halfSize;
        private OnScreenJoystick _joystick;

        void Awake()
        {
            _joystick = GetComponent<OnScreenJoystick>();
            // Update the size of the visual
            _halfSize = Size / 2;
            _backgroundRing.rectTransform.sizeDelta = new Vector2(Size, Size);
            _dot.rectTransform.sizeDelta = new Vector2(_halfSize, _halfSize);
        }

        void Update()
        {
            // Update the position of the backgroundRing based on the joystick's PointerDownPosition
            _backgroundRing.rectTransform.anchoredPosition = _joystick.PointerDownPosition;
            var delta = _joystick.RealPointerPosition - _joystick.PointerDownPosition;
            delta = Vector2.ClampMagnitude(delta, _halfSize);
            _dot.rectTransform.anchoredPosition = _joystick.PointerDownPosition + delta;
        }
    }
}
