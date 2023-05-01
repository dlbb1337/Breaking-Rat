using UnityEngine;
using UnityEngine.EventSystems;

namespace GameConsole.UI
{
    public class ResizePoint :
    MonoBehaviour,
    IPointerDownHandler,
    IDragHandler,
    IPointerEnterHandler,
    IPointerExitHandler
    {
        private enum ResizeDirection
        {
            Top,
            Left,
            Bottom,
            Right,
            Topleft,
            Topright,
            Bottomleft,
            Bottomright
        }

        [SerializeField] private Vector2 _minSize = new Vector2(200, 200);
        [SerializeField] private Vector2 _maxSize = new Vector2(1920, 1080);
        [SerializeField] private RectTransform _rectToResize;
        [SerializeField] private RectTransform _locale;
        [SerializeField] private ResizeDirection _resizeDirection;
        [SerializeField] private Texture2D _onPointerEnterCursor;

        private Vector2 _previousPointerPosition;
        private Vector2 _currentPointerPosition;

        public void OnPointerExit(PointerEventData eventData)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Cursor.SetCursor(_onPointerEnterCursor, Vector2.zero, CursorMode.Auto);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            switch (_resizeDirection)
            {
                case ResizeDirection.Left:
                    _rectToResize.SetPivot(new Vector2(1, 0.5f));
                    break;

                case ResizeDirection.Top:
                    _rectToResize.SetPivot(new Vector2(0.5f, 0));
                    break;

                case ResizeDirection.Right:
                    _rectToResize.SetPivot(new Vector2(0, 0.5f));
                    break;

                case ResizeDirection.Bottom:
                    _rectToResize.SetPivot(new Vector2(0.5f, 1));
                    break;

                case ResizeDirection.Topleft:
                    _rectToResize.SetPivot(new Vector2(1, 0));
                    break;
                case ResizeDirection.Topright:
                    _rectToResize.SetPivot(new Vector2(0, 0));
                    break;

                case ResizeDirection.Bottomright:
                    _rectToResize.SetPivot(new Vector2(0, 1));
                    break;

                case ResizeDirection.Bottomleft:
                    _rectToResize.SetPivot(new Vector2(1, 1));
                    break;
            }

            RectTransformUtility.ScreenPointToLocalPointInRectangle
                (_locale, eventData.position, null, out _previousPointerPosition);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_rectToResize is null)
            {
                return;
            }

            Vector2 sizeDelta = _rectToResize.sizeDelta;

            RectTransformUtility.ScreenPointToLocalPointInRectangle
                (_locale, eventData.position, null, out _currentPointerPosition);

            Vector2 deltaPointerPosition = _currentPointerPosition - _previousPointerPosition;

            switch (_resizeDirection)
            {
                case ResizeDirection.Left:
                    deltaPointerPosition.y = 0;
                    deltaPointerPosition.x = -deltaPointerPosition.x;
                    break;

                case ResizeDirection.Top:
                    deltaPointerPosition.x = 0;
                    break;

                case ResizeDirection.Right:
                    deltaPointerPosition.y = 0;
                    break;

                case ResizeDirection.Bottom:
                    deltaPointerPosition.x = 0;
                    deltaPointerPosition.y = -deltaPointerPosition.y;
                    break;

                case ResizeDirection.Topleft:
                    deltaPointerPosition.x = -deltaPointerPosition.x;
                    break;

                case ResizeDirection.Topright:
                    break;

                case ResizeDirection.Bottomright:
                    deltaPointerPosition.y = -deltaPointerPosition.y;
                    break;

                case ResizeDirection.Bottomleft:
                    deltaPointerPosition = -deltaPointerPosition;
                    break;
            }

            sizeDelta += new Vector2(deltaPointerPosition.x, deltaPointerPosition.y);

            sizeDelta = new Vector2(
                Mathf.Clamp(sizeDelta.x, _minSize.x, _maxSize.x),
                Mathf.Clamp(sizeDelta.y, _minSize.y, _maxSize.y)
                );

            _rectToResize.sizeDelta = sizeDelta;
            _previousPointerPosition = _currentPointerPosition;
        }
    }
}
