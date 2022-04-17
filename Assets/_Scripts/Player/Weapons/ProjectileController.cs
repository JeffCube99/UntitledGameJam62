using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private List<GameObjectRuntimeSet> universeRuntimeSets;
    [SerializeField] private GameState gameState;
    [SerializeField] private ObjectPool onDestroyParticlesObjectPool;
    private int previousRuntimeSet;
    public bool isDestroyed;

    public void DestroyProjectile()
    {
        isDestroyed = true;
        gameObject.SetActive(false);
        if (onDestroyParticlesObjectPool != null)
            onDestroyParticlesObjectPool.Instantiate(transform.position, transform.rotation);
    }

    public void OnSpawn()
    {
        isDestroyed = false;
        universeRuntimeSets[previousRuntimeSet].Remove(gameObject);
        universeRuntimeSets[gameState.currentUniverseIndex].Add(gameObject);
        previousRuntimeSet = gameState.currentUniverseIndex;
    }

    public void OnDestroy()
    {
        universeRuntimeSets[previousRuntimeSet].Remove(gameObject);
    }
}
