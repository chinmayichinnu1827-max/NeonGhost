using UnityEngine;

public class DetectionManager : MonoBehaviour
{
    public static DetectionManager Instance { get; private set; }

    [Header("Detection")]
    [Range(0f, 100f)]
    public float detectionLevel = 0f;

    public float detectionIncreaseRate = 25f;
    public float detectionDecreaseRate = 15f;

    [Header("State")]
    public bool alarmTriggered = false;

    public float DetectionLevel => detectionLevel;
    public bool AlarmTriggered => alarmTriggered;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void AddDetection(float amount)
    {
        if (alarmTriggered)
            return;

        detectionLevel = Mathf.Clamp(
            detectionLevel + amount, 0f, 100f
        );

        if (detectionLevel >= 100f)
        {
            TriggerAlarm();
        }
    }

    public void ReduceDetection(float amount)
    {
        if (alarmTriggered)
            return;

        detectionLevel = Mathf.Clamp(
            detectionLevel - amount, 0f, 100f
        );
    }

    public void TriggerAlarm()
    {
        if (alarmTriggered)
            return;

        alarmTriggered = true;
        Debug.Log("SECURITY ALERT: GHOST DETECTED!");
    }

    public void ResetSecurity()
    {
        detectionLevel = 0f;
        alarmTriggered = false;
    }
}