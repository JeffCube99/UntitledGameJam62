using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAppearanceController : MonoBehaviour
{
    [SerializeField] private Animator characterAnimator;
    [SerializeField] private PlayerState playerState;
    private float lastHorizontalInput;
    private float lastVerticalInput;
    [SerializeField] private SpriteRenderer playerSpriteRenderer;
    [SerializeField] private float timeBetweenDamageFlashes;
    [SerializeField] private Color damagedColor;
    [SerializeField] private Color regularColor;
    [SerializeField] private Color deadColor;
    private Rigidbody2D playerRigidbody;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        SetAnimatorMovementVariables(playerRigidbody.velocity);
    }

    private void SetAnimatorMovementVariables(Vector2 direction)
    {
        Vector2 normalizedDirection = direction.normalized;
        if (direction.magnitude > 0.1f)
        {
            lastHorizontalInput = normalizedDirection.x;
            lastVerticalInput = normalizedDirection.y;
            characterAnimator.SetFloat("last_x", lastHorizontalInput);
            characterAnimator.SetFloat("last_y", lastVerticalInput);
        }
        characterAnimator.SetFloat("velocity_x", normalizedDirection.x);
        characterAnimator.SetFloat("velocity_y", normalizedDirection.y);
        characterAnimator.SetFloat("speed", direction.magnitude);
    }

    public void OnPlayerDeath()
    {
        StopAllCoroutines();
        characterAnimator.SetBool("isDead", true);
        SetAnimatorMovementVariables(Vector2.zero);
        ShowDeadForm();
    }

    public void OnPlayerRespawn()
    {
        StopAllCoroutines();
        ShowRegularForm();
    }

    public void OnDamageTaken()
    {
        StartDamageEffect();
    }

    private void StartDamageEffect()
    {
        StopAllCoroutines();
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(DamageEffect());
        }
    }

    private void ShowRegularForm()
    {
        playerSpriteRenderer.color = regularColor;
    }

    private void ShowDamagedForm()
    {
        playerSpriteRenderer.color = damagedColor;
    }

    private void ShowDeadForm()
    {
        playerSpriteRenderer.color = deadColor;
    }

    IEnumerator DamageEffect()
    {
        float timePassed = 0;
        while (timePassed < playerState.damagedInvincibilityDuration)
        {
            ShowDamagedForm();
            yield return new WaitForSeconds(timeBetweenDamageFlashes);
            ShowRegularForm();
            yield return new WaitForSeconds(timeBetweenDamageFlashes);
            timePassed += timeBetweenDamageFlashes * 2;
        }
    }
}
