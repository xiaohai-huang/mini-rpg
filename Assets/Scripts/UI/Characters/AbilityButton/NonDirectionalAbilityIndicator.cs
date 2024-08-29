using UnityEngine;

namespace Xiaohai.UI
{
    public class NonDirectionalAbilityIndicator : AbilityIndicator
    {
        [SerializeField]
        private Transform _indicator;

        [SerializeField]
        [Range(0.5f, 30f)]
        private float _radius;

        [ColorUsage(true, true)]
        [SerializeField]
        private Color _activeColor;

        [ColorUsage(true, true)]
        [SerializeField]
        private Color _cancelColor;

        private Material _indicatorMaterial;

        void Awake()
        {
            _indicator.gameObject.SetActive(false);

            UpdateSize();

            _indicatorMaterial = _indicator.GetComponent<MeshRenderer>().material;
            SetColor(_activeColor);
        }

        void Update()
        {
            if (_button.Cancelling || !_ability.CanPerform)
            {
                SetColor(_cancelColor);
            }
            else
            {
                SetColor(_activeColor);
            }
        }

        protected override void OnBeginInteraction()
        {
            _indicator.gameObject.SetActive(true);
        }

        protected override void OnReleased(bool released)
        {
            _indicator.gameObject.SetActive(false);
        }

        private void SetColor(Color color)
        {
            _indicatorMaterial.SetColor("_Color", color);
        }

        private void UpdateSize()
        {
            // update the size of the range indicator
            _indicator.transform.localScale = new Vector3(_radius * 2, _radius * 2, 1);
        }

#if UNITY_EDITOR
        void OnDrawGizmosSelected()
        {
            UpdateSize();
        }
#endif
    }
}
