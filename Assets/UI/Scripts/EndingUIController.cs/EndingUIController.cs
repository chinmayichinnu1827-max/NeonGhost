using UnityEngine;
using TMPro;

public class EndingUIController : MonoBehaviour
{
    public MissionManager missionManager;
    public GameObject endingPanel;
    public TMP_Text endingTitle;
    public TMP_Text endingDescription;

    public void ExposeNexus()
    {
        ChooseEnding(MissionManager.Ending.ExposeNexus);
    }

    public void TakeControl()
    {
        ChooseEnding(MissionManager.Ending.TakeControl);
    }

    private void ChooseEnding(MissionManager.Ending ending)
    {
        if (missionManager == null ||
            !missionManager.ArchiveCollected ||
            missionManager.MissionComplete)
        {
            Debug.LogWarning(
                "The archive must be collected before choosing an ending.");
            return;
        }

        missionManager.ChooseEnding(ending);

        if (!missionManager.MissionComplete)
            return;

        if (endingPanel != null)
            endingPanel.SetActive(true);

        if (endingTitle != null)
        {
            endingTitle.text = ending ==
                MissionManager.Ending.ExposeNexus
                ? "THE TRUTH IS OUT"
                : "THE GRID IS YOURS";
        }

        if (endingDescription != null)
        {
            endingDescription.text = ending ==
                MissionManager.Ending.ExposeNexus
                ? "NEXUS has been exposed. The city will know the truth."
                : "You have seized control of the identity network.";
        }
    }
}