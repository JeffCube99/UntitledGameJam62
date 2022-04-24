using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiverseCrystalUIManager : MonoBehaviour
{
    [SerializeField] private GameObject crystal0;
    [SerializeField] private GameObject crystal1;
    [SerializeField] private PlayerState playerState;

    public void updateCrystalUI()
    {
        if (playerState.universeCrystals.Contains(0))
        {
            crystal0.SetActive(true);
        }
        else
        {
            crystal0.SetActive(false);
        }

        if (playerState.universeCrystals.Contains(1))
        {
            crystal1.SetActive(true);
        }
        else
        {
            crystal1.SetActive(false);
        }
    }
}
