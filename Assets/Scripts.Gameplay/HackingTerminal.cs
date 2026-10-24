using UnityEngine;

public class HackingTerminal : MonoBehaviour
{
    [Header("Terminal Settings")]
    public string terminalName = "NEXUS Terminal";

    public PlayerIdentity.AccessLevel identityGranted =
        PlayerIdentity.AccessLevel.Employee;

    public KeyCode interactKey = KeyCode.E;

    [Header("Interaction")]
    public float interactionDistance = 3f;

    private Transform player;
    private PlayerIdentity playerIdentity;
    private bool hacked;

    void Update()
    {
        if (hacked || player == null || playerIdentity == null)
            return;

        float distance = Vector3.Distance(
            transform.position,
            player.position
        );

        if (distance > interactionDistance)
            return;

        if (Input.GetKeyDown(interactKey))
        {
            HackTerminal();
        }
    }

    void HackTerminal()
    {
        if (playerIdentity == null)
            return;

        playerIdentity.currentAccess = identityGranted;
        hacked = true;

        Debug.Log("IDENTITY ACQUIRED: " + identityGranted);
        Debug.Log(terminalName + " hacked successfully!");
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerIdentity identity =
            other.GetComponentInParent<PlayerIdentity>();

        if (identity == null)
            return;

        player = identity.transform;
        playerIdentity = identity;

        Debug.Log("Terminal nearby: " + terminalName);
        Debug.Log("Press " + interactKey + " to hack.");
    }

    void OnTriggerExit(Collider other)
    {
        PlayerIdentity identity =
            other.GetComponentInParent<PlayerIdentity>();

        if (identity == null || identity != playerIdentity)
            return;

        player = null;
        playerIdentity = null;
    }
}