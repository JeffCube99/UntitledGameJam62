using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private List<GameObjectRuntimeSet> universeRuntimeSets;
    [SerializeField] private GameState gameState;
    private int previousRuntimeSet;
    public void DestroyProjectile()
    {
        gameObject.SetActive(false);
    }

    public void OnSpawn()
    {
        universeRuntimeSets[previousRuntimeSet].Remove(gameObject);
        universeRuntimeSets[gameState.currentUniverseIndex].Add(gameObject);
        previousRuntimeSet = gameState.currentUniverseIndex;
    }

    public void OnDestroy()
    {
        universeRuntimeSets[previousRuntimeSet].Remove(gameObject);
    }
}
