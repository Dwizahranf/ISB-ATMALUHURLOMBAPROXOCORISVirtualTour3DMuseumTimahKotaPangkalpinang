using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public RectTransform joystickHandle;
    private RectTransform joystickBase;
    private Vector2 joystickCenter;
    private bool isDragging = false;

    void Start()
    {
        joystickBase = GetComponent<RectTransform>();
        joystickCenter = joystickBase.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            Vector2 newPosition = Vector2.zero;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBase, eventData.position, eventData.pressEventCamera, out newPosition);
            joystickHandle.localPosition = newPosition;

            // Clamp the handle position within the bounds of the joystick base
            joystickHandle.localPosition = Vector2.ClampMagnitude(newPosition, joystickBase.rect.width / 2f);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        // Reset handle position to center when the touch/mouse is released
        joystickHandle.localPosition = Vector2.zero;
    }
}
