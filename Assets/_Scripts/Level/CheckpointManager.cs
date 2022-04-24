using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] private GameState gameState;
    [SerializeField] private PlayerState playerState;
    [SerializeField] private GameObject respawnPoint;
    [SerializeField] private GameObject checkpointText;
    [SerializeField] private TextMeshPro checkpointTextMesh;
    [SerializeField] private int universeIndex;
    private int playerInCounter;
    public UnityEvent OnProgressSaved;

    private void Start()
    {
        checkpointText.SetActive(false);
    }


    private CheckpointData GenerateCheckpointData()
    {
        CheckpointData checkpointData = new CheckpointData();
        checkpointData.spawnPosition = respawnPoint.transform.position;
        checkpointData.universeIndex = universeIndex;
        checkpointData.playerUniverseCrystals = playerState.universeCrystals;
        checkpointData.hasWeapon = playerState.hasWeapon;
        return checkpointData;
    }

    public void SetCheckpoint()
    {
        if (playerInCounter >= 1)
        {
            gameState.checkpointData = GenerateCheckpointData();
            DisplayText("Progress Saved!");
            OnProgressSaved.Invoke();
        }
    }

    private void DisplayText(string message)
    {
        checkpointTextMesh.text = message;
        checkpointText.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerInCounter += 1;
            if (playerInCounter == 1)
            {
                DisplayText("Press [e]\nTo Save");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerInCounter -= 1;
            if (playerInCounter < 1)
            {
                checkpointText.SetActive(false);
            }
        }
    }
}
