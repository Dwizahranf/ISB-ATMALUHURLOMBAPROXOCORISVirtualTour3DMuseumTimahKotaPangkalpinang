using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIZoomImage : MonoBehaviour, IPointerClickHandler
{
   private RectTransform imageRectTransform;
    private Vector2 originalSize;
    private Vector2 originalPosition;

    private bool isZoomed = false;

    // Adjustable zoom size and position
    public Vector2 adjustableZoomSize = new Vector2(2967f, 1126.8f);
    public Vector2 adjustableZoomPosition = new Vector2(-426f, 169f);

    // Reference to other images to hide
    public Image[] imagesToHide;

    void Start()
    {
        imageRectTransform = GetComponent<RectTransform>();
        originalSize = imageRectTransform.sizeDelta;
        originalPosition = imageRectTransform.anchoredPosition;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isZoomed)
        {
            // Hide other images
            HideOtherImages();

            ZoomIn();
        }
        else
        {
            ShowOtherImages(); // Show other images when zooming out
            ZoomOut();
        }
    }

    private void HideOtherImages()
    {
        foreach (Image image in imagesToHide)
        {
            image.gameObject.SetActive(false);
        }
    }

    private void ShowOtherImages()
    {
        foreach (Image image in imagesToHide)
        {
            image.gameObject.SetActive(true);
        }
    }

    private void ZoomIn()
    {
        isZoomed = true;
        imageRectTransform.sizeDelta = adjustableZoomSize;
        imageRectTransform.anchoredPosition = adjustableZoomPosition;
    }

    private void ZoomOut()
    {
        isZoomed = false;
        imageRectTransform.sizeDelta = originalSize;
        imageRectTransform.anchoredPosition = originalPosition;
    }
}