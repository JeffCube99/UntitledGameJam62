using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStateManager : MonoBehaviour
{
    [SerializeField] private PlayerState playerState;
    [SerializeField] private GameState gameState;

    public UnityEvent OnPlayerPickupWeapon;
    public UnityEvent OnPlayerPickupGem;

    // Start is called before the first frame update
    void Start()
    {
        ResetPlayer();
    }

    public void PlayerPickupWeapon()
    {
        playerState.hasWeapon = true;
        OnPlayerPickupWeapon.Invoke();
    }

    public void PlayerPickupGem(int universeIndex)
    {
        playerState.universeCrystals.Add(universeIndex);
        OnPlayerPickupGem.Invoke();
    }

    public void ResetPlayer()
    {
        playerState.isInvincible = true;
        playerState.health = playerState.maxHealth;
        playerState.isDead = false;
        playerState.isTraveling = false;
        playerState.universeCrystals = gameState.checkpointData.playerUniverseCrystals;
        playerState.hasWeapon = gameState.checkpointData.hasWeapon;
        playerState.isInvincible = false;
    }

}
