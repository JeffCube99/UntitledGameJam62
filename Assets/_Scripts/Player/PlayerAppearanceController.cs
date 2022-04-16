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

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (!playerState.isDead)
        {
            Vector2 direction = context.ReadValue<Vector2>();
            SetAnimatorMovementVariables(direction);
        }
    }

    private void SetAnimatorMovementVariables(Vector2 direction)
    {
        if (direction.x != lastHorizontalInput || direction.y != lastVerticalInput)
        {
            characterAnimator.SetFloat("last_x", lastHorizontalInput);
            characterAnimator.SetFloat("last_y", lastVerticalInput);
        }
        lastHorizontalInput = direction.x;
        lastVerticalInput = direction.y;
        characterAnimator.SetFloat("velocity_x", direction.x);
        characterAnimator.SetFloat("velocity_y", direction.y);
        characterAnimator.SetFloat("speed", direction.magnitude);
    }

    public void OnPlayerDeath()
    {
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
