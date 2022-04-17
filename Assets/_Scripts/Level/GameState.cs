using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New GameState", menuName = "ScriptableObjects/States/GameState")]

public class GameState : ScriptableObject
{
    [SerializeField] private ObjectPool playerObjectPool;
    public CheckpointData checkpointData;
    public UnityEvent OnPlayerRespawn;
    public int currentUniverseIndex;

    public void respawnPlayer()
    {
        playerObjectPool.Instantiate(checkpointData.spawnPosition, Quaternion.identity);
        currentUniverseIndex = checkpointData.universeIndex;
        OnPlayerRespawn.Invoke();
    }
}
