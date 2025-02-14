using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    [SerializeField] private FirstPersonController firstPersonController;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Interaction Started");
            firstPersonController.DisableLookAround();
            firstPersonController.DisableMovement();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Interaction Ended");
            firstPersonController.EnableLookAround();
            firstPersonController.EnableMovement();
        }
    }
}
