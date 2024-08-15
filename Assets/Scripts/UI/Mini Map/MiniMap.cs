using Core.Game.Common;
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

        [SerializeField]
        private Vec2EventChannel _viewBoxPositionEventChannel;

        [SerializeField]
        private VoidEventChannel _startMoveViewBoxEventChannel;

        [SerializeField]
        private VoidEventChannel _endMoveViewBoxEventChannel;

        [SerializeField]
        private RuntimeCharacterAnchor _player;

        public float MoveSpeed = 0.5f;
        float horizontalMax;
        float horizontalMax2;
        float verticalMax;
        float verticalMax2;

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
            horizontalMax2 = horizontalMax * 2f;
            verticalMax2 = verticalMax * 2f;
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
            if (!CheckInputType(type) || MoveSpeed == 0 || _player.Value == null)
                return;
            // place at the player's position
            var playerPosition = MapPlayerPositionToMiniMapPosition();

            _viewBox.anchoredPosition = Clamp(playerPosition);
            _viewBox.gameObject.SetActive(true);
            UpdateNormalizedPosition();
            _startMoveViewBoxEventChannel.RaiseEvent();
        }

        void HandleOnMoving(OnScreenInputEventChannel.Input type, PointerEventData eventData)
        {
            if (!CheckInputType(type) || MoveSpeed == 0)
                return;
            var delta = eventData.delta;
            Vector2 dest = _viewBox.anchoredPosition + delta * MoveSpeed;
            // keep the box inside the map

            _viewBox.anchoredPosition = Clamp(dest);
            UpdateNormalizedPosition();
        }

        Vector2 Clamp(Vector2 value)
        {
            value.x = Mathf.Clamp(value.x, -horizontalMax, horizontalMax);
            value.y = Mathf.Clamp(value.y, -verticalMax, verticalMax);
            return value;
        }

        void HandleOnReleased(OnScreenInputEventChannel.Input type)
        {
            if (!CheckInputType(type) || MoveSpeed == 0)
                return;
            // _viewBox.anchoredPosition = Vector2.zero;
            _viewBox.gameObject.SetActive(false);
            UpdateNormalizedPosition();
            _endMoveViewBoxEventChannel.RaiseEvent();
        }

        void UpdateNormalizedPosition()
        {
            var position = _viewBox.anchoredPosition;
            var normalizedPosition = new Vector2(
                position.x / horizontalMax2,
                position.y / verticalMax2
            );
            _viewBoxPositionEventChannel.RaiseEvent(normalizedPosition);
        }

        bool CheckInputType(OnScreenInputEventChannel.Input type) =>
            type == OnScreenInputEventChannel.Input.VIEW_MOVE;

        Vector2 MapPlayerPositionToMiniMapPosition()
        {
            var halfWorldMapSize = Constants.MAP_SIZE / 2f;
            var playerWorldPosition = new Vector2(
                _player.Value.transform.position.x,
                _player.Value.transform.position.z
            );
            // Normalize the position to -1, 1
            var pos = playerWorldPosition / halfWorldMapSize;

            // Convert to mini map scale
            Vector2 miniMapScale = new Vector2(horizontalMax, verticalMax);
            return pos * miniMapScale;
        }
    }
}
