using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDController : MonoBehaviour
{
    [Header("Gameplay References")]
    public PlayerIdentity playerIdentity;
    public GhostReplay ghostReplay;
    public DetectionManager detectionManager;
    public MissionManager missionManager;

    [Header("HUD Text")]
    public TMP_Text identityText;
    public TMP_Text objectiveText;
    public TMP_Text ghostStatusText;
    public TMP_Text securityStatusText;

    [Header("Progress Bars")]
    public Image detectionFill;
    public Image ghostCooldownFill;

    [Header("Panels")]
    public GameObject securityAlertPanel;
    public GameObject missionCompletePanel;

    void Start()
    {
        if (securityAlertPanel != null)
            securityAlertPanel.SetActive(false);

        if (missionCompletePanel != null)
            missionCompletePanel.SetActive(false);
    }

    void Update()
    {
        UpdateIdentity();
        UpdateGhost();
        UpdateSecurity();
        UpdateMission();
    }

    void UpdateIdentity()
    {
        if (identityText == null)
            return;

        if (playerIdentity == null)
        {
            identityText.text = "IDENTITY: UNKNOWN";
            return;
        }

        identityText.text =
            "IDENTITY: " + playerIdentity.CurrentAccess.ToString()
                .ToUpperInvariant();
    }

    void UpdateGhost()
    {
        if (ghostReplay == null)
            return;

        if (ghostStatusText != null)
        {
            if (ghostReplay.IsRecording)
                ghostStatusText.text = "RECORDING...";
            else if (ghostReplay.IsPlaying)
                ghostStatusText.text = "REPLAY ACTIVE";
            else if (ghostReplay.IsOnCooldown)
                ghostStatusText.text =
                    "COOLDOWN // " +
                    ghostReplay.CooldownRemaining.ToString("0.0") + "s";
            else
                ghostStatusText.text = "READY // PRESS Q";
        }

        if (ghostCooldownFill != null)
        {
            float duration = Mathf.Max(
                ghostReplay.cooldownDuration, 0.01f);

            float remaining =
                ghostReplay.CooldownRemaining / duration;

            ghostCooldownFill.fillAmount =
                1f - Mathf.Clamp01(remaining);
        }
    }

    void UpdateSecurity()
    {
        if (detectionManager == null)
            return;

        float level = Mathf.Clamp01(
            detectionManager.DetectionLevel / 100f);

        if (detectionFill != null)
            detectionFill.fillAmount = level;

        if (securityStatusText != null)
        {
            securityStatusText.text =
                detectionManager.AlarmTriggered
                ? "LOCKDOWN INITIATED"
                : level > 0.65f
                    ? "SECURITY BREACH IMMINENT"
                    : level > 0.05f
                        ? "SCANNING"
                        : "SYSTEM MONITORING";
        }

        if (securityAlertPanel != null)
        {
            securityAlertPanel.SetActive(
                detectionManager.AlarmTriggered);
        }
    }

    void UpdateMission()
    {
        if (missionManager == null)
            return;

        if (objectiveText != null)
        {
            if (missionManager.MissionComplete)
                objectiveText.text = "MISSION COMPLETE";
            else if (missionManager.ArchiveCollected)
                objectiveText.text = "OBJECTIVE: REACH EXTRACTION";
            else
                objectiveText.text = "OBJECTIVE: STEAL NEXUS ARCHIVE";
        }

        if (missionCompletePanel != null)
        {
            missionCompletePanel.SetActive(
                missionManager.MissionComplete);
        }
    }
}