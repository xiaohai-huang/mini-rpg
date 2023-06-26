using UnityEngine;
using UnityEngine.UI;

namespace Xiaohai.Input
{
    public class JoystickVisual : MonoBehaviour
    {
        [SerializeField] private Image _backgroundRing;
        [SerializeField] private Image _dot;
        [SerializeField] private InputReader _inputReader;
        private OnScreenJoystick _joystick;

        void Start()
        {
            _joystick = GetComponent<OnScreenJoystick>();
        }

        // Update is called once per frame
        void Update()
        {
            _backgroundRing.rectTransform.anchoredPosition = _joystick.PointerDownPosition;
            _dot.rectTransform.anchoredPosition = _joystick.PointerPosition;
        }
    }
}
