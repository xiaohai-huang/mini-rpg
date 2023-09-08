using UnityEngine;

namespace Xiaohai.UI
{
    public abstract class AbilityIndicator : MonoBehaviour
    {
        [SerializeField] private OnScreenInputEventChannel _onScreenInputEventChannel;
        [SerializeField] private OnScreenInputEventChannel.Input _inputType;

        protected AbilityButton _button;

        void OnEnable()
        {
            _button = _onScreenInputEventChannel.GetButton(_inputType);
            _button.OnBeginInteraction += OnBeginInteraction;
            _button.OnMoving += OnMoving;
            _button.OnReleased += OnReleased;
            _button.OnCancellingChanged += OnCancellingChanged;
        }

        void OnDisable()
        {
            _button.OnBeginInteraction -= OnBeginInteraction;
            _button.OnMoving -= OnMoving;
            _button.OnReleased -= OnReleased;
            _button.OnCancellingChanged -= OnCancellingChanged;
        }

        protected virtual void OnMoving()
        {

        }

        protected virtual void OnReleased(bool released)
        {

        }

        protected virtual void OnBeginInteraction()
        {

        }

        protected virtual void OnCancellingChanged(bool cancelling)
        {

        }
    }
}
