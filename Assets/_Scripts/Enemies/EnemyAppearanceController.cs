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
    private Rigidbody2D enemyRigidbody2D;

    private void Start()
    {
        enemyRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (enemyRigidbody2D != null)
            SetAnimatorMovementVariables(enemyRigidbody2D.velocity);
    }

    private void SetAnimatorMovementVariables(Vector2 velocity)
    {
        if (velocity.x < 0)
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
        enemySpriteRenderer.sortingOrder = 1;
        StopAllCoroutines();
        ShowRegularForm();
    }

    public void OnDeath()
    {
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
