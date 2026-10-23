using UnityEngine;

public class SecurityTarget : MonoBehaviour
{
    public enum TargetType
    {
        Player,
        Ghost
    }

    public TargetType targetType = TargetType.Player;
}