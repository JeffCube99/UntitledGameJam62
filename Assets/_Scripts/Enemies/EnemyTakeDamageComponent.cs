using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakeDamageComponent : MonoBehaviour
{
    public EnemyStateController enemyState;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleHazardCollision(collision);
    }

    private void HandleHazardCollision(Collider2D other)
    {
        HazardComponent otherHazard = other.gameObject.GetComponent<HazardComponent>();
        if (otherHazard != null && otherHazard.hazard.harmsEnemy)
        {
            enemyState.TakeDamage(otherHazard.hazard.damage);
        }
    }
}
