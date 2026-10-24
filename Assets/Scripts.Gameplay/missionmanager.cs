using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public enum Ending
    {
        None,
        ExposeNexus,
        TakeControl
    }

    public static MissionManager Instance { get; private set; }

    public bool ArchiveCollected { get; private set; }

    public Ending SelectedEnding { get; private set; } = Ending.None;

    public bool MissionComplete => SelectedEnding != Ending.None;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void CollectArchive()
    {
        if (ArchiveCollected)
            return;

        ArchiveCollected = true;

        Debug.Log("Mission: Archive collected!");
    }

    public void ChooseEnding(Ending ending)
    {
        if (!ArchiveCollected || MissionComplete || ending == Ending.None)
            return;

        SelectedEnding = ending;

        Debug.Log("Mission complete! Ending: " + ending);
    }
}