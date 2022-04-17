using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSignManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogueText;
    private int playerInCounter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerInCounter += 1;
            if (playerInCounter == 1)
            {
                dialogueText.SetActive(true);
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
                dialogueText.SetActive(false);
            }
        }
    }
}
