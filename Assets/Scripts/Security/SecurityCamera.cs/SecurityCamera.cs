using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    [Header("Scanning")]
    public Transform cameraHead;
    public float scanAngle = 60f;
    public float scanSpeed = 30f;

    [Header("Detection")]
    public float viewDistance = 12f;
    [Range(1f, 180f)]
    public float viewAngle = 60f;
    public LayerMask obstacleMask;

    [Header("Detection Rates")]
    public float playerDetectionRate = 25f;
    public float ghostDetectionRate = 8f;

    private Quaternion startRotation;
    private bool scanningForward = true;

    private void Start()
    {
        if (cameraHead == null)
            cameraHead = transform;

        startRotation = cameraHead.localRotation;
    }

    private void Update()
    {
        ScanCamera();

        if (DetectionManager.Instance == null)
            return;

        SecurityTarget target = FindVisibleTarget();

        if (target == null)
        {
            DetectionManager.Instance.ReduceDetection(
                DetectionManager.Instance.detectionDecreaseRate
                * Time.deltaTime
            );
            return;
        }

        if (target.targetType ==
            SecurityTarget.TargetType.Player)
        {
            DetectionManager.Instance.AddDetection(
                playerDetectionRate * Time.deltaTime
            );
        }
        else
        {
            // The holographic decoy distracts security,
            // but it does not trigger the full player alarm.
            DetectionManager.Instance.ReduceDetection(
                ghostDetectionRate * Time.deltaTime
            );
        }
    }

    private void ScanCamera()
    {
        float angle = Mathf.PingPong(
            Time.time * scanSpeed, scanAngle * 2f
        ) - scanAngle;

        cameraHead.localRotation =
            startRotation * Quaternion.Euler(0f, angle, 0f);
    }

    private SecurityTarget FindVisibleTarget()
    {
        SecurityTarget[] targets =
            FindObjectsByType<SecurityTarget>(
                FindObjectsSortMode.None
            );

        SecurityTarget bestTarget = null;
        float closestDistance = float.MaxValue;

        foreach (SecurityTarget target in targets)
        {
            if (target == null)
                continue;

            Vector3 direction =
                target.transform.position - cameraHead.position;

            float distance = direction.magnitude;

            if (distance > viewDistance || distance < 0.01f)
                continue;

            float angle = Vector3.Angle(
                cameraHead.forward, direction
            );

            if (angle > viewAngle * 0.5f)
                continue;

            if (Physics.Raycast(
                cameraHead.position,
                direction.normalized,
                out RaycastHit hit,
                distance,
                obstacleMask,
                QueryTriggerInteraction.Ignore))
            {
                // A wall or other obstacle blocks detection.
                if (hit.transform != target.transform &&
                    !hit.transform.IsChildOf(target.transform))
                {
                    continue;
                }
            }

            if (distance < closestDistance)
            {
                closestDistance = distance;
                bestTarget = target;
            }
        }

        return bestTarget;
    }

    private void OnDrawGizmosSelected()
    {
        Transform head = cameraHead != null
            ? cameraHead
            : transform;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(
            head.position,
            head.forward * viewDistance
        );
    }
}