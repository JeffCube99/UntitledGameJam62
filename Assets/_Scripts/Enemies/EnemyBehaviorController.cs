using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyBehaviorController : MonoBehaviour
{
    [SerializeField] private AIDestinationSetter AIDestinationSetter;
    [SerializeField] private EnemyStateController enemyState;
    private bool seekingPlayer;

    public void ResetSeeker()
    {
        StopAllCoroutines();
        AIDestinationSetter.target = transform;
        seekingPlayer = false;
    }

    public void OnDamageTaken()
    {
        GameObject playerGameObject = GameObject.FindGameObjectWithTag("Player");
        if (playerGameObject != null && !seekingPlayer && !enemyState.isDead)
        {
            seekingPlayer = true;
            StartCoroutine(SeekPlayer(playerGameObject));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !seekingPlayer && !enemyState.isDead)
        {
            seekingPlayer = true;
            StartCoroutine(SeekPlayer(collision.gameObject));
        }
    }

    IEnumerator SeekPlayer(GameObject playerGameObject)
    {
        while (!HasLineOfSight(playerGameObject))
        {
            yield return null;
        }
        AIDestinationSetter.target = playerGameObject.transform;
    }

    private bool HasLineOfSight(GameObject otherGameObject)
    {
        Vector2 direction = (Vector2)(otherGameObject.transform.position - transform.position);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction.normalized);
        Debug.Log($"RaycastAtPlayer {hit.collider}");
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;

    }
}
