using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Video;

public class videotaptap : MonoBehaviour, IPointerClickHandler
{
    private VideoPlayer videoPlayer; // Reference to VideoPlayer component
    private bool isPlaying = false;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>(); // Get VideoPlayer component attached to this GameObject
        videoPlayer.Stop(); // Ensure video player starts paused
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isPlaying)
        {
            PlayVideo();
        }
        else
        {
            PauseVideo();
        }
    }

    private void PlayVideo()
    {
        videoPlayer.Play();
        isPlaying = true;
    }

    private void PauseVideo()
    {
        videoPlayer.Pause();
        isPlaying = false;
    }
}