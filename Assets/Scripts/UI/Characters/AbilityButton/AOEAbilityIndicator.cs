using UnityEngine;

namespace Xiaohai.UI
{
    public class AOEAbilityIndicator : AbilityIndicator
    {
        [SerializeField]
        private Transform _rangeIndicator;

        [SerializeField]
        private Transform _AOE_Indicator;

        [SerializeField]
        [Range(0.5f, 30f)]
        [Tooltip("Range radius")]
        private float _radius;

        [SerializeField]
        [Range(0.1f, 30f)]
        [Tooltip("AOE radius")]
        private float _AOE_Radius;

        [SerializeField]
        private Color _activeColor;

        [SerializeField]
        private Color _cancelColor;

        private Material _rangeIndicatorMaterial;
        private Material _AOE_IndicatorMaterial;

        void Awake()
        {
            _rangeIndicator.gameObject.SetActive(false);
            _AOE_Indicator.gameObject.SetActive(false);

            UpdateSize();

            _rangeIndicatorMaterial = _rangeIndicator
                .GetComponentInChildren<MeshRenderer>()
                .material;
            _AOE_IndicatorMaterial = _AOE_Indicator.GetComponentInChildren<MeshRenderer>().material;
            SetColor(_activeColor);
        }

        protected override void OnMoving()
        {
            _rangeIndicator.gameObject.SetActive(true);
            var buttonPos = _button.Position;
            _AOE_Indicator.localPosition = new Vector3(buttonPos.x, 0, buttonPos.y) * _radius;
        }

        protected override void OnBeginInteraction()
        {
            _AOE_Indicator.localPosition = Vector3.zero;
            _rangeIndicator.gameObject.SetActive(false);
            _AOE_Indicator.gameObject.SetActive(true);
        }

        protected override void OnReleased(bool released)
        {
            _rangeIndicator.gameObject.SetActive(false);
            _AOE_Indicator.gameObject.SetActive(false);
        }

        protected override void OnCancellingChanged(bool cancelling)
        {
            // turn red if the player is trying to cancel the action
            SetColor(cancelling ? _cancelColor : _activeColor);
        }

        private void SetColor(Color color)
        {
            _rangeIndicatorMaterial.SetColor("_Color", color);
            _AOE_IndicatorMaterial.SetColor("_Color", color);
        }

        private void UpdateSize()
        {
            // update the size of the range indicator
            _rangeIndicator.transform.localScale = new Vector3(_radius * 2, 1, _radius * 2);
            _AOE_Indicator.transform.localScale = new Vector3(_AOE_Radius * 2, 1, _AOE_Radius * 2);
        }

#if UNITY_EDITOR
        void OnDrawGizmosSelected()
        {
            UpdateSize();
        }
#endif
    }
}
