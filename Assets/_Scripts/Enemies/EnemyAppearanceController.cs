using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAppearanceController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer enemySpriteRenderer;
    [SerializeField] private Animator enemyAnimator;
    [SerializeField] private float timeBetweenDamageFlashes;
    [SerializeField] private float damageEffectDuration;
    [SerializeField] private Color damagedColor;
    [SerializeField] private Color regularColor;
    [SerializeField] private Color deadColor;
    [SerializeField] private AudioSource walkAudioSource;
    private Vector2 lastPosition;

    private void Start()
    {
        if (enemyAnimator != null)
            enemyAnimator.keepAnimatorControllerStateOnDisable = true;
    }

    private void FixedUpdate()
    {
        if (enemyAnimator != null)
        {
            Vector2 direction = (Vector2)transform.position - lastPosition;
            lastPosition = (Vector2)transform.position;
            SetAnimatorMovementVariables(direction);
        }
    }

    private void SetAnimatorMovementVariables(Vector2 direction)
    {
        if (direction.x < 0)
        {
            enemyAnimator.SetFloat("velocity_x", -1);
        }
        else
        {
            enemyAnimator.SetFloat("velocity_x", 1);
        }
    }

    public void OnRespawn()
    {
        enemySpriteRenderer.sortingOrder = 0;
        StopAllCoroutines();
        ShowRegularForm();
        if (enemyAnimator != null)
            enemyAnimator.SetBool("is_dead", false);
        if (walkAudioSource != null && !walkAudioSource.isPlaying)
            walkAudioSource.Play();
    }

    public void OnDeath()
    {
        if (walkAudioSource != null && walkAudioSource.isPlaying)
            walkAudioSource.Stop();
        enemySpriteRenderer.sortingOrder = -1;
        if (enemyAnimator != null)
            enemyAnimator.SetBool("is_dead", true);
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
