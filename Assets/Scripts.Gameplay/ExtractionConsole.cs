using UnityEngine;

public class ExtractionConsole : MonoBehaviour
{
    private bool playerNearby;
    private PlayerIdentity playerIdentity;

    void Update()
    {
        if (!playerNearby || playerIdentity == null)
            return;

        MissionManager mission = MissionManager.Instance;

        if (mission == null || !mission.ArchiveCollected || mission.MissionComplete)
            return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            mission.ChooseEnding(MissionManager.Ending.ExposeNexus);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            mission.ChooseEnding(MissionManager.Ending.TakeControl);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerIdentity identity = other.GetComponentInParent<PlayerIdentity>();
        if (identity == null)
            return;

        playerIdentity = identity;
        playerNearby = true;
        Debug.Log("Extraction available. Collect the archive, then press 1 or 2.");
    }

    void OnTriggerExit(Collider other)
    {
        PlayerIdentity identity = other.GetComponentInParent<PlayerIdentity>();
        if (identity == null || identity != playerIdentity)
            return;

        playerNearby = false;
        playerIdentity = null;
    }
}
