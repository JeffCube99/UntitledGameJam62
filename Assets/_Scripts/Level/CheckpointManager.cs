using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] private GameState gameState;
    [SerializeField] private GameObject respawnPoint;
    [SerializeField] private GameObject checkpointText;
    [SerializeField] private TextMesh checkpointTextMesh;
    [SerializeField] private int universeIndex;
    private int playerInCounter;
    private CheckpointData checkpointData;

    private void Start()
    {
        checkpointData = new CheckpointData();
        checkpointData.spawnPosition = respawnPoint.transform.position;
        checkpointData.universeIndex = universeIndex;
    }

    public void SetCheckpoint()
    {
        if (playerInCounter >= 1)
        {
            gameState.checkpointData = checkpointData;
            DisplayText("Progress Saved!");
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
