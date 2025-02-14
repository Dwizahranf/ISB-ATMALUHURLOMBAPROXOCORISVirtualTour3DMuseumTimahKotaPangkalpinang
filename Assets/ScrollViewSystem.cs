using UnityEngine;
using UnityEngine.UI;

public class ScrollViewSystem : MonoBehaviour
{
    private ScrollRect _scrollRect;


    [SerializeField] private ScrollButton _leftButton;
    [SerializeField] private ScrollButton _rightButton;
    [SerializeField] private ScrollButton _buttomButton;
    [SerializeField] private ScrollButton _topButton;

    [SerializeField] private float scrollSpeed = 0.01f;

    void Start()
    {
       _scrollRect = GetComponent<ScrollRect>();  
    }

    // Update is called once per frame
    void Update()
    {
        if(_leftButton != null)
        {
            if(_leftButton.isDown)
            {
                ScrollLeft();
            }
        }
         if(_rightButton != null)
        {
            if(_rightButton.isDown)
            {
                ScrollRight();
            }
        }
         if(_buttomButton != null)
        {
            if(_buttomButton.isDown)
            {
                ScrollBottom();
            }
        }
         if(_topButton != null)
        {
            if(_topButton.isDown)
            {
                ScrollTop();
            }
        }
        
    }

    private void ScrollLeft()
    {
        if(_scrollRect != null)
        {
            if(_scrollRect.horizontalNormalizedPosition >= 0f)
            {
                _scrollRect.horizontalNormalizedPosition -= scrollSpeed;
            }
        }
    }

    private void ScrollRight()
    {
        if(_scrollRect != null)
        {
            if(_scrollRect.horizontalNormalizedPosition <= 0f)
            {
                _scrollRect.horizontalNormalizedPosition -= scrollSpeed;
            }
        }
    }

    private void ScrollTop()
    {
        if(_scrollRect != null)
        {
            if(_scrollRect.horizontalNormalizedPosition <= 1f)
            {
                _scrollRect.horizontalNormalizedPosition -= scrollSpeed;
            }
        }
    }

    private void ScrollBottom()
    {
        if(_scrollRect != null)
        {
            if(_scrollRect.horizontalNormalizedPosition >= 0f)
            {
                _scrollRect.horizontalNormalizedPosition -= scrollSpeed;
            }
        }
    }
}
