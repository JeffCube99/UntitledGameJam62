using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    [SerializeField] private PlayerState playerState;

    // Start is called before the first frame update
    void Start()
    {
        playerState.health = playerState.maxHealth;   
        playerState.isInvincible = false;   
    }

}
