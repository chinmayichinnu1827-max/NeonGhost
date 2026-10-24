
using UnityEngine;

public class PlayerIdentity : MonoBehaviour
{
    public enum AccessLevel
    {
        None = 0,
        Employee = 1,
        Admin = 2
    }

    [SerializeField]
    public AccessLevel currentAccess = AccessLevel.None;

    public AccessLevel CurrentAccess => currentAccess;

    public bool HasAccess(AccessLevel required)
    {
        return currentAccess >= required;
    }

    public void StealIdentity(AccessLevel level)
    {
        if (level <= currentAccess)
        {
            Debug.Log("You already have this access level.");
            return;
        }

        currentAccess = level;
        Debug.Log("IDENTITY ACQUIRED: " + currentAccess);
    }
}