using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Core.Game.UI
{
    [RequireComponent(typeof(Image))]
    public class OnScreenViewMover : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField]
        private OnScreenInputEventChannel _inputEventChannel;
        private readonly OnScreenInputEventChannel.Input _type = OnScreenInputEventChannel
            .Input
            .VIEW_MOVE;

        public void OnBeginDrag(PointerEventData eventData)
        {
            _inputEventChannel.RaiseBeginInteractionEvent(_type);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _inputEventChannel.RaiseOnMovingEvent(_type, eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _inputEventChannel.RaiseReleaseEvent(_type);
        }
    }
}
