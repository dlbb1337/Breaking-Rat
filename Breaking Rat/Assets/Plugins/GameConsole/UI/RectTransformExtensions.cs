using UnityEngine;

namespace GameConsole.UI
{
    public static class RectTransformExtensions
    {
        public static void SetPivot(this RectTransform rectTransform, Vector2 pivotOffset)
        {
            if (rectTransform is null)
            {
                return;
            }

            Vector2 size = rectTransform.rect.size;
            Vector2 deltaPivot = rectTransform.pivot - pivotOffset;
            Vector3 deltaPosition = new Vector3(deltaPivot.x * size.x, deltaPivot.y * size.y);

            rectTransform.pivot = pivotOffset;
            rectTransform.localPosition -= deltaPosition;
        }
    }
}
