using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New GameState", menuName = "ScriptableObjects/States/GameState")]

public class GameState : ScriptableObject
{
    [SerializeField] private ObjectPool playerObjectPool;
    [SerializeField] private List<GameObjectRuntimeSet> universeRuntimeSets;

    public CheckpointData checkpointData;
    public UnityEvent OnPlayerRespawn;
    public int currentUniverseIndex;

    public void respawnPlayer()
    {
        playerObjectPool.Instantiate(checkpointData.spawnPosition, Quaternion.identity);
        currentUniverseIndex = checkpointData.universeIndex;
        respawnEnemiesAndClearProjectiles();
        OnPlayerRespawn.Invoke();
    }

    public void respawnEnemiesAndClearProjectiles()
    {
        for (int i = 0; i < universeRuntimeSets.Count; i++)
        {
            GameObjectRuntimeSet runtimeSet = universeRuntimeSets[i];
            foreach (GameObject universeObject in runtimeSet.items)
            {
                // respawn enemies
                EnemyStateController enemyStateController = universeObject.GetComponent<EnemyStateController>();
                if (enemyStateController != null)
                {
                    universeObject.SetActive(true);
                    enemyStateController.Respawn();
                    universeObject.SetActive(false);
                }

                // destroy projectiles
                ProjectileController projectileController = universeObject.GetComponent<ProjectileController>();
                if (projectileController != null)
                {
                    Destroy(universeObject);
                }
            }
        }
    }
}
