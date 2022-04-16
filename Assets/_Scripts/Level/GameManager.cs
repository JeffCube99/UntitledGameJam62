using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameState gameState;
    [SerializeField] private GameObject initialSpawnPoint;
    [SerializeField] private int initialUniverseIndex;

    // Start is called before the first frame update
    void Start()
    {
        CheckpointData startCheckpoint = new CheckpointData();
        startCheckpoint.spawnPosition = initialSpawnPoint.transform.position;
        startCheckpoint.universeIndex = initialUniverseIndex;
        gameState.checkpointData = startCheckpoint;
        gameState.respawnPlayer();
    }
}
