using UnityEngine;

public class ScrollSprite : MonoBehaviour
{
    public RectTransform spriteTransform;
    public RectTransform textTransform;

    void Update()
    {
        // Adjust sprite position based on text scrolling
        spriteTransform.anchoredPosition = new Vector2(spriteTransform.anchoredPosition.x, textTransform.anchoredPosition.y);
    }
}