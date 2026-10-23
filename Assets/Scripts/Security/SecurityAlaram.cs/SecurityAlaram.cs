using UnityEngine;

public class SecurityAlarm : MonoBehaviour
{
    public Light alarmLight;
    public AudioSource alarmAudio;

    private bool alarmWasActive;

    private void Update()
    {
        if (DetectionManager.Instance == null)
            return;

        bool active =
            DetectionManager.Instance.AlarmTriggered;

        if (active == alarmWasActive)
            return;

        alarmWasActive = active;
        ApplyAlarmState(active);
    }

    private void ApplyAlarmState(bool active)
    {
        if (alarmLight != null)
            alarmLight.enabled = active;

        if (alarmAudio != null)
        {
            if (active && !alarmAudio.isPlaying)
                alarmAudio.Play();
            else if (!active && alarmAudio.isPlaying)
                alarmAudio.Stop();
        }

        if (active)
            Debug.Log("Facility lockdown initiated!");
    }
}