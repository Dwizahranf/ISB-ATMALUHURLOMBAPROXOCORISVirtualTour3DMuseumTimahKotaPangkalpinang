using UnityEngine;

public class CameraController : MonoBehaviour
{
   public float cameraSpeed = 1.0f;
   public bool cameraMovementEnabled = true;

    void Update()
    {
        // Periksa apakah gerakan kamera diaktifkan
        if (cameraMovementEnabled)
        {
            // Implementasikan logika untuk menggerakkan kamera di sini
            float verticalInput = Input.GetAxis("Vertical");
            transform.Translate(0, verticalInput * cameraSpeed * Time.deltaTime, 0);
        }
    }

    // Metode untuk mengaktifkan atau menonaktifkan gerakan kamera
    public void ToggleCameraMovement(bool enable)
    {
        cameraMovementEnabled = enable;
    }
}