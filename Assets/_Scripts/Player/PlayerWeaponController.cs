using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    [SerializeField] private PlayerState playerState;
    [SerializeField] private GameState gameState;
    [SerializeField] private Weapon playerWeapon;

    public void StartFiringPlayerWeapon()
    {
        if (!playerState.isDead && !playerState.isTraveling && !gameState.gameIsPaused)
            playerWeapon.Fire();
    }


    public void StopFiringPlayerWeapon()
    {
        playerWeapon.CeaseFire();
    }

}
