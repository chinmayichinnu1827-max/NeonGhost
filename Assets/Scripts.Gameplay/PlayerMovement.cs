using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
[Header("Movement Settings")]
public float moveSpeed = 5f;
public float gravity = -9.81f;


[Header("References")]
public Transform cameraTransform;
public Animator animator;

private CharacterController controller;
private Vector3 velocity;

void Start()
{
    controller = GetComponent<CharacterController>();

    if (controller == null)
    {
        Debug.LogError("Player needs a CharacterController component!");
        enabled = false;
        return;
    }

    if (cameraTransform == null && Camera.main != null)
    {
        cameraTransform = Camera.main.transform;
    }

    if (animator == null)
    {
        animator = GetComponentInChildren<Animator>();
    }
}

void Update()
{
    if (controller == null)
        return;

    float x = Input.GetAxisRaw("Horizontal");
    float z = Input.GetAxisRaw("Vertical");

    Vector3 forward = transform.forward;
    Vector3 right = transform.right;

    if (cameraTransform != null)
    {
        forward = cameraTransform.forward;
        right = cameraTransform.right;
    }

    forward.y = 0f;
    right.y = 0f;

    forward.Normalize();
    right.Normalize();

    Vector3 move = forward * z + right * x;

    if (move.magnitude > 1f)
        move.Normalize();

    controller.Move(move * moveSpeed * Time.deltaTime);

    if (controller.isGrounded && velocity.y < 0f)
    {
        velocity.y = -2f;
    }

    velocity.y += gravity * Time.deltaTime;
    controller.Move(velocity * Time.deltaTime);

    if (animator != null)
    {
        animator.SetFloat("Speed", move.magnitude);
    }
}

}