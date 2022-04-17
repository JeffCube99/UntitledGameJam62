using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Hazard", menuName = "ScriptableObjects/Enums/Hazard")]
public class Hazard : ScriptableObject
{
    public bool harmsPlayer;
    public bool harmsEnemy;
    public int damage;
}
