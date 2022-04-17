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
    public int currentUniverseIndex;
    public bool gameIsPaused;

    public UnityEvent OnPlayerRespawn;
    public UnityEvent OnGamePause;
    public UnityEvent OnGameResume;

    public void PauseGame()
    {
        if (!gameIsPaused)
        {
            gameIsPaused = true;
            Time.timeScale = 0;
            OnGamePause.Invoke();
        }
    }

    public void ResumeGame()
    {
        if (gameIsPaused)
        {
            gameIsPaused = false;
            Time.timeScale = 1;
            OnGameResume.Invoke();
        }
    }

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
