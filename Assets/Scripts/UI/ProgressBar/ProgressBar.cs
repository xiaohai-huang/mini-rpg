using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Xiaohai.UI
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField]
        private Image _content;

        [SerializeField]
        private Image _divider;

        [SerializeField]
        private Color _barColor;
        public Color BarColor
        {
            get => _barColor;
            set
            {
                _content.color = value;
                _barColor = value;
            }
        }
        public int MaxValue;
        private int _currentValue;
        public int CurrentValue
        {
            get => _currentValue;
            set { _currentValue = Math.Clamp(value, 0, MaxValue); }
        }
        public int DividerAmount = 1000;
        public bool ShowDivider = true;

        [SerializeField]
        private float _speed;

        private readonly List<GameObject> _dividers = new List<GameObject>();

        void Awake()
        {
            if (ShowDivider)
                SetupDivider();
        }

        // Update is called once per frame
        void Update()
        {
            _content.fillAmount = Mathf.Lerp(
                _content.fillAmount,
                (float)_currentValue / MaxValue,
                _speed * Time.deltaTime
            );
        }

        void SetupDivider()
        {
            var size = _content.rectTransform.rect;
            int numDividers = MaxValue / DividerAmount;
            for (int i = 0; i < numDividers; i++)
            {
                var divider = Instantiate(_divider, _content.transform);
                divider.gameObject.SetActive(true);

                divider.rectTransform.sizeDelta = new Vector2(
                    divider.rectTransform.rect.width,
                    size.height / 2
                );
                divider.rectTransform.anchoredPosition = new Vector2(
                    -(i + 1) * (size.width * DividerAmount) / MaxValue,
                    0f
                );
                _dividers.Add(divider.gameObject);
            }
        }

        public void SetValues(int currentValue, int maxValue)
        {
            if (MaxValue != maxValue)
            {
                MaxValue = maxValue;
                if (ShowDivider)
                {
                    _dividers.ForEach(divider => Destroy(divider));
                    SetupDivider();
                }
            }
            CurrentValue = currentValue;
        }

#if UNITY_EDITOR
        void OnDrawGizmosSelected()
        {
            _content.color = BarColor;
        }
#endif
    }
}
