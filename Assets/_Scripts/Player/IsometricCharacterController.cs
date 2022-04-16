using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IsometricCharacterController : MonoBehaviour
{
    public float speed;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
    [SerializeField] private PlayerState playerState;
    private Rigidbody2D characterRigidbody2D;
    private Vector3 acceleration = Vector3.zero;
    private Vector2 targetDirection;

    private void Start()
    {
        characterRigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (!playerState.isDead)
        {
            targetDirection = context.ReadValue<Vector2>();
        }
    }

    public void StopPlayerMovement()
    {
        targetDirection = Vector2.zero;
    }

    private void FixedUpdate()
    {
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        Vector3 targetVelocity = targetDirection.normalized * speed;
        Vector3 dampedVelocity = Vector3.SmoothDamp(characterRigidbody2D.velocity, targetVelocity, ref acceleration, m_MovementSmoothing);
        Vector3 velocityDelta = dampedVelocity - (Vector3)characterRigidbody2D.velocity;
        characterRigidbody2D.AddForce(velocityDelta * characterRigidbody2D.mass / Time.fixedDeltaTime, ForceMode2D.Force);
    }
}
