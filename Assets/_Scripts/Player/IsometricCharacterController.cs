using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IsometricCharacterController : MonoBehaviour
{
    public float speed;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement

    private Rigidbody2D characterRigidbody2D;
    private Vector3 acceleration = Vector3.zero;
    private float horizontalInput;
    private float verticalInput;

    private void Start()
    {
        characterRigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector3 direction = context.ReadValue<Vector2>();
        horizontalInput = direction.x;
        verticalInput = direction.y;
    }

    private void FixedUpdate()
    {
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        Vector3 targetVelocity = new Vector2(horizontalInput, verticalInput).normalized * speed;
        Vector3 dampedVelocity = Vector3.SmoothDamp(characterRigidbody2D.velocity, targetVelocity, ref acceleration, m_MovementSmoothing);
        Vector3 velocityDelta = dampedVelocity - (Vector3)characterRigidbody2D.velocity;
        characterRigidbody2D.AddForce(velocityDelta * characterRigidbody2D.mass / Time.fixedDeltaTime, ForceMode2D.Force);
    }
}
