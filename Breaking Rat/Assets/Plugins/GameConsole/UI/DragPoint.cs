using UnityEngine;
using UnityEngine.EventSystems;

namespace GameConsole.UI
{
    public class DragPoint : MonoBehaviour, IDragHandler, IPointerDownHandler
    {
        [SerializeField] private RectTransform _rectToDrag;
        [SerializeField] private RectTransform _locale;
        [SerializeField] private Vector2 _dragPoint;

        public void OnPointerDown(PointerEventData eventData)
        {
            _rectToDrag.SetPivot(_dragPoint);
        }

        public void OnDrag(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle
                (_locale, eventData.position, null, out Vector2 pointerPosition);

            _rectToDrag.localPosition = pointerPosition;
        }
    }
}
