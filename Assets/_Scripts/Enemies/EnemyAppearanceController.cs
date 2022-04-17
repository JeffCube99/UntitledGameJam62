using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAppearanceController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer enemySpriteRenderer;
    [SerializeField] private float timeBetweenDamageFlashes;
    [SerializeField] private float damageEffectDuration;
    [SerializeField] private Color damagedColor;
    [SerializeField] private Color regularColor;
    [SerializeField] private Color deadColor;

    private void FixedUpdate()
    {
        //SetAnimatorMovementVariables(Vector2 direction);
    }

    private void SetAnimatorMovementVariables(Vector2 direction)
    {
        //if (direction.x != lastHorizontalInput || direction.y != lastVerticalInput)
        //{
        //    characterAnimator.SetFloat("last_x", lastHorizontalInput);
        //    characterAnimator.SetFloat("last_y", lastVerticalInput);
        //}
        //lastHorizontalInput = direction.x;
        //lastVerticalInput = direction.y;
        //characterAnimator.SetFloat("velocity_x", direction.x);
        //characterAnimator.SetFloat("velocity_y", direction.y);
        //characterAnimator.SetFloat("speed", direction.magnitude);
    }

    public void OnRespawn()
    {
        StopAllCoroutines();
        ShowRegularForm();
    }

    public void OnDeath()
    {
        // characterAnimator.SetBool("isDead", true);
        // SetAnimatorMovementVariables(Vector2.zero);
        StopAllCoroutines();
        ShowDeadForm();
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
        enemySpriteRenderer.color = regularColor;
    }

    private void ShowDamagedForm()
    {
        enemySpriteRenderer.color = damagedColor;
    }

    private void ShowDeadForm()
    {
        enemySpriteRenderer.color = deadColor;
    }

    IEnumerator DamageEffect()
    {
        float timePassed = 0;
        while (timePassed < damageEffectDuration)
        {
            ShowDamagedForm();
            yield return new WaitForSeconds(timeBetweenDamageFlashes);
            ShowRegularForm();
            yield return new WaitForSeconds(timeBetweenDamageFlashes);
            timePassed += timeBetweenDamageFlashes * 2;
        }
    }
}
