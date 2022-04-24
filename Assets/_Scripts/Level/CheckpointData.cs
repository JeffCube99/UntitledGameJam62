using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CheckpointData
{
    public Vector3 spawnPosition;
    public int universeIndex;
    public List<int> playerUniverseCrystals;
    public bool hasWeapon;
}
