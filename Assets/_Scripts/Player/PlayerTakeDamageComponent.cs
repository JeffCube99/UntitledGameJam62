using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakeDamageComponent : MonoBehaviour
{
    public PlayerState playerState;
    [SerializeField] private float damageEffectDuration;
    [SerializeField] private float timeBetweenDamageFlashes;
    [SerializeField] private SpriteRenderer playerSpriteRenderer;
    [SerializeField] private Color damagedColor;
    [SerializeField] private Color regularColor;
    [SerializeField] private CapsuleCollider2D playerDamageCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!playerState.isInvincible)
        {
            HandleHazardCollision(collision);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private void HandleHazardCollision(Collider2D other)
    {
        HazardComponent otherHazard = other.gameObject.GetComponent<HazardComponent>();
        if (otherHazard != null)
        {
            playerState.TakeDamage(otherHazard.hazard.playerDamage);
        }
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

    IEnumerator DamageEffect()
    {
        playerState.isInvincible = true;
        playerDamageCollider.enabled = false;
        float timePassed = 0;
        while (timePassed < damageEffectDuration)
        {
            ShowDamagedForm();
            yield return new WaitForSeconds(timeBetweenDamageFlashes);
            ShowRegularForm();
            yield return new WaitForSeconds(timeBetweenDamageFlashes);
            timePassed += timeBetweenDamageFlashes * 2;
        }
        playerState.isInvincible = false;
        playerDamageCollider.enabled = true;
    }
}
