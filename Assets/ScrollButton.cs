using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isDown = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        isDown = true;   
    }

    // Update is called once per frame
    public void OnPointerUp(PointerEventData eventData)
    {
        isDown = false;
    }
}
