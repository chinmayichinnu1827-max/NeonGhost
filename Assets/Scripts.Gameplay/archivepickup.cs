using UnityEngine;

public class ArchivePickup : MonoBehaviour
{
    [Header("Archive Settings")]
    public KeyCode interactKey = KeyCode.E;

    private bool playerNearby;
    private bool collected;

    private Transform player;
    private PlayerIdentity playerIdentity;

    void Update()
    {
        if (!playerNearby || collected || player == null)
            return;

        if (Input.GetKeyDown(interactKey))
        {
            Collect();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerIdentity identity =
            other.GetComponentInParent<PlayerIdentity>();

        if (identity == null)
            return;

        player = identity.transform;
        playerIdentity = identity;
        playerNearby = true;

        Debug.Log("Archive nearby. Press E to collect.");
    }

    void OnTriggerExit(Collider other)
    {
        PlayerIdentity identity =
            other.GetComponentInParent<PlayerIdentity>();

        if (identity == null || identity != playerIdentity)
            return;

        playerNearby = false;
        player = null;
        playerIdentity = null;
    }

    void Collect()
    {
        if (playerIdentity == null || collected)
            return;

        if (MissionManager.Instance == null)
        {
            Debug.LogError("MissionManager is missing from the scene!");
            return;
        }

        MissionManager.Instance.CollectArchive();

        collected = true;

        Debug.Log("Archive collected successfully!");

        gameObject.SetActive(false);
    }
}