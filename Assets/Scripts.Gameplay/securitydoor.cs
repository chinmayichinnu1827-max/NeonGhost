using UnityEngine;

public class SecurityDoor : MonoBehaviour
{
    public PlayerIdentity.AccessLevel requiredAccess =
        PlayerIdentity.AccessLevel.Employee;

    public float openHeight = 3f;
    public float openSpeed = 2f;
    public KeyCode interactKey = KeyCode.E;

    private PlayerIdentity playerIdentity;
    private bool playerNearby;
    private bool isOpen;

    private Vector3 closedPosition;
    private Vector3 openPosition;

    void Start()
    {
        closedPosition = transform.position;
        openPosition = closedPosition + Vector3.up * openHeight;
    }

    void Update()
    {
        if (isOpen)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                openPosition,
                openSpeed * Time.deltaTime
            );
            return;
        }

        if (playerNearby &&
            playerIdentity != null &&
            Input.GetKeyDown(interactKey))
        {
            CheckAccess();
        }
    }

    public void CheckAccess()
    {
        if (playerIdentity == null)
        {
            Debug.LogWarning("Player identity not found near the door!");
            return;
        }

        if (playerIdentity.HasAccess(requiredAccess))
        {
            isOpen = true;
            Debug.Log("Access granted! Door opening.");
        }
        else
        {
            Debug.Log("Access denied! Required access: " + requiredAccess);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerIdentity identity =
            other.GetComponentInParent<PlayerIdentity>();

        if (identity == null)
            return;

        playerIdentity = identity;
        playerNearby = true;

        Debug.Log("Door nearby. Press E.");
    }

    void OnTriggerExit(Collider other)
    {
        PlayerIdentity identity =
            other.GetComponentInParent<PlayerIdentity>();

        if (identity == null || identity != playerIdentity)
            return;

        playerIdentity = null;
        playerNearby = false;
    }
}
