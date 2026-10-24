using UnityEngine;

public class SecurityDoorTriggerRelay : MonoBehaviour
{
    private SecurityDoor securityDoor;

    private void Awake()
    {
        securityDoor = GetComponentInParent<SecurityDoor>();

        if (securityDoor == null)
        {
            Debug.LogError("SecurityDoor not found on the parent object!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            securityDoor?.CheckAccess();
        }
    }
}