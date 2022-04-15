using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthUIController : MonoBehaviour
{
    [SerializeField] private List<GameObject> HeartContainers;
    [SerializeField] private PlayerState playerState;

    private void Start()
    {
        UpdatePlayerHealth();
    }

    public void UpdatePlayerHealth()
    {
        for (int i = 0; i < HeartContainers.Count; i++)
        {
            if (i < playerState.health)
            {
                HeartContainers[i].SetActive(true);
            }
            else
            {
                HeartContainers[i].SetActive(false);
            }
        }
    }
}
