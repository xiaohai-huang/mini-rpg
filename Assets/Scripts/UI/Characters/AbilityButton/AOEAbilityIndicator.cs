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
        private Transform _final_AOE_Indicator;

        [SerializeField]
        [Range(0, 20f)]
        private float Sensitivity;

        [SerializeField]
        [Range(0.5f, 30f)]
        [Tooltip("Range radius")]
        private float _radius;

        [SerializeField]
        [Range(0.1f, 30f)]
        [Tooltip("AOE radius")]
        private float _AOE_Radius;

        [SerializeField]
        [ColorUsage(true, true)]
        private Color _activeColor;

        [SerializeField]
        [ColorUsage(true, true)]
        private Color _cancelColor;

        private Material _rangeIndicatorMaterial;
        private Material _AOE_IndicatorMaterial;

        void Awake()
        {
            _rangeIndicator.gameObject.SetActive(false);
            _AOE_Indicator.gameObject.SetActive(false);
            _final_AOE_Indicator.gameObject.SetActive(false);

            UpdateSize();

            _rangeIndicatorMaterial = _rangeIndicator
                .GetComponentInChildren<MeshRenderer>()
                .material;
            _AOE_IndicatorMaterial = _AOE_Indicator.GetComponentInChildren<MeshRenderer>().material;
            SetColor(_activeColor);
        }

        Vector2 _position;
        bool _moving;

        void Update()
        {
            if (_moving)
            {
                _position = Vector2.MoveTowards(
                    _position,
                    _button.Position,
                    Sensitivity * Time.deltaTime
                );
                _AOE_Indicator.localPosition = new Vector3(_position.x, 0, _position.y) * _radius;
            }
            if (_button.Cancelling || !_ability.CanPerform)
            {
                SetColor(_cancelColor);
            }
            else
            {
                SetColor(_activeColor);
            }
        }

        protected override void OnMoving()
        {
            _moving = true;
            _rangeIndicator.gameObject.SetActive(true);
        }

        protected override void OnBeginInteraction()
        {
            _AOE_Indicator.localPosition = Vector3.zero;
            _position = Vector2.zero;
            _rangeIndicator.gameObject.SetActive(false);
            _AOE_Indicator.gameObject.SetActive(true);
            _final_AOE_Indicator.gameObject.SetActive(false);
        }

        protected override async void OnReleased(bool released)
        {
            _moving = false;
            _rangeIndicator.gameObject.SetActive(false);
            await Awaitable.NextFrameAsync();
            SetColor(_activeColor);
            _AOE_Indicator.gameObject.SetActive(false);
            var buttonPos = _button.Position;
            _final_AOE_Indicator.localPosition = new Vector3(buttonPos.x, 0, buttonPos.y) * _radius;

            _final_AOE_Indicator.gameObject.SetActive(true);

            await Awaitable.WaitForSecondsAsync(0.1f);
            _final_AOE_Indicator.gameObject.SetActive(false);
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
            _final_AOE_Indicator.transform.localScale = new Vector3(
                _AOE_Radius * 2,
                1,
                _AOE_Radius * 2
            );
        }

#if UNITY_EDITOR
        void OnDrawGizmosSelected()
        {
            UpdateSize();
        }
#endif
    }
}
