using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Core.Game.UI
{
    public class MiniMap : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _viewBox;
        private float _viewBoxThickness;

        [SerializeField]
        private OnScreenInputEventChannel _inputEventChannel;

        public float MoveSpeed = 0.5f;
        float horizontalMax;
        float verticalMax;

        void Awake()
        {
            _viewBoxThickness = _viewBox.GetComponent<OutlineRectGraphic>().Thickness;
        }

        void Start()
        {
            _viewBox.gameObject.SetActive(false);
            // Get map size and compute view box allowed range
            RectTransform map = (RectTransform)transform;
            var mapSize = map.rect.size;
            var viewBoxSize = _viewBox.rect.size + (_viewBoxThickness * 2f * Vector2.one);
            horizontalMax = mapSize.x / 2f - viewBoxSize.x / 2f;
            verticalMax = mapSize.y / 2f - viewBoxSize.y / 2f;
        }

        void OnEnable()
        {
            _inputEventChannel.OnBeginInteractionEventRaised += HandleOnBeginInteraction;
            _inputEventChannel.OnMovingEventRaised += HandleOnMoving;
            _inputEventChannel.OnReleasedEventRaised += HandleOnReleased;
        }

        void OnDisable()
        {
            _inputEventChannel.OnBeginInteractionEventRaised -= HandleOnBeginInteraction;
            _inputEventChannel.OnMovingEventRaised -= HandleOnMoving;
            _inputEventChannel.OnReleasedEventRaised -= HandleOnReleased;
        }

        void HandleOnBeginInteraction(OnScreenInputEventChannel.Input type)
        {
            _viewBox.anchoredPosition = Vector2.zero;
            _viewBox.gameObject.SetActive(true);
            // place at the player's position
        }

        void HandleOnMoving(OnScreenInputEventChannel.Input type, PointerEventData eventData)
        {
            var delta = eventData.delta;
            Vector2 dest = _viewBox.anchoredPosition + delta * MoveSpeed;
            // keep the box inside the map
            dest.x = Mathf.Clamp(dest.x, -horizontalMax, horizontalMax);
            dest.y = Mathf.Clamp(dest.y, -verticalMax, verticalMax);

            _viewBox.anchoredPosition = dest;
        }

        void HandleOnReleased(OnScreenInputEventChannel.Input type)
        {
            _viewBox.gameObject.SetActive(false);
        }
    }
}
