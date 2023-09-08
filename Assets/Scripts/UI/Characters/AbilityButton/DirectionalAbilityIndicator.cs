using UnityEngine;

namespace Xiaohai.UI
{
    public class DirectionalAbilityIndicator : AbilityIndicator
    {
        [SerializeField] private Transform _arrowIndicator;
        [SerializeField] private Transform _rangeIndicator;
        [SerializeField] private Transform _arrowBody;
        [SerializeField] private Transform _arrowHead;
        [SerializeField] private Transform _circle;
        [SerializeField] private Color _activeColor;
        [SerializeField] private Color _cancelColor;
        private Material _arrowBodyMaterial;
        private Material _arrowHeadMaterial;
        private Material _rangeIndicatorMaterial;

        [SerializeField] private Transform _target;
        [SerializeField] private float _length;

        void Awake()
        {
            _arrowIndicator.gameObject.SetActive(false);

            // update the length of the body and position of the head
            var newScale = _arrowBody.transform.localScale;
            newScale.y = _length;
            _arrowBody.transform.localScale = newScale;

            var newPosition = _arrowBody.transform.localPosition;
            newPosition.z = _length / 2;
            _arrowBody.transform.localPosition = newPosition;

            var newHeadPosition = _arrowHead.transform.localPosition;
            newHeadPosition.z = _length;
            _arrowHead.transform.localPosition = newHeadPosition;

            // update the size of the range indicator
            _circle.transform.localScale = new Vector3(_length * 2, _length * 2, 1);


            _arrowBodyMaterial = _arrowBody.GetComponent<MeshRenderer>().material;
            _arrowHeadMaterial = _arrowHead.GetComponent<MeshRenderer>().material;
            _rangeIndicatorMaterial = _circle.GetComponent<MeshRenderer>().material;
            SetColor(_activeColor);
        }

        protected override void OnMoving()
        {
            var dir = new Vector3(_button.Direction.x, 0, _button.Direction.y);
            _arrowIndicator.rotation = Quaternion.LookRotation(dir, Vector3.up);
        }

        protected override void OnReleased(bool released)
        {
            _arrowIndicator.gameObject.SetActive(false);
            _rangeIndicator.gameObject.SetActive(false);
        }

        protected override void OnBeginInteraction()
        {
            if (_target != null)
            {
                _arrowIndicator.rotation = _target.transform.rotation;
            }
            _arrowIndicator.gameObject.SetActive(true);
            _rangeIndicator.gameObject.SetActive(true);
        }

        protected override void OnCancellingChanged(bool cancelling)
        {
            // turn red if the player is trying to cancel the action
            SetColor(cancelling ? _cancelColor : _activeColor);
        }

        private void SetColor(Color color)
        {
            _arrowBodyMaterial.color = color;
            _arrowHeadMaterial.color = color;
            _rangeIndicatorMaterial.SetColor("_Color", color);
        }
    }
}