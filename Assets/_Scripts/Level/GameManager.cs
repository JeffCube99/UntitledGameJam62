using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameState gameState;
    [SerializeField] private CheckpointData initialCheckpoint;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera virtualCamera;

    private void Awake()
    {
        gameState.checkpointData = initialCheckpoint;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameState.gameIsPaused = false;
        gameState.respawnPlayer();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        virtualCamera.Follow = player.transform;
    }

    public void PauseGame()
    {
        gameState.PauseGame();
    }

    public void ResumeGame()
    {
        gameState.ResumeGame();
    }
}
