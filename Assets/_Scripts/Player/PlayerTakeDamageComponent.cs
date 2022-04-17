using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakeDamageComponent : MonoBehaviour
{
    public PlayerState playerState;
    [SerializeField] private CapsuleCollider2D playerDamageCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!playerState.isInvincible)
        {
            HandleHazardCollision(collision);
        }
    }

    private void HandleHazardCollision(Collider2D other)
    {
        HazardComponent otherHazard = other.gameObject.GetComponent<HazardComponent>();
        if (otherHazard != null && otherHazard.hazard.harmsPlayer)
        {
            playerState.TakeDamage(otherHazard.hazard.damage);
        }
    }

    public void OnDamageTaken()
    {
        StartDamageEffect();
    }

    private void StartDamageEffect()
    {
        if (!playerState.isDead)
        {
            StopAllCoroutines();
            if (gameObject.activeInHierarchy)
            {
                StartCoroutine(DamageEffect());
            }
        }
    }

    IEnumerator DamageEffect()
    {
        playerState.isInvincible = true;
        playerDamageCollider.enabled = false;
        yield return new WaitForSeconds(playerState.damagedInvincibilityDuration);
        playerState.isInvincible = false;
        playerDamageCollider.enabled = true;
    }
}
