using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    [SerializeField] private PlayerState playerState;
    [SerializeField] private Weapon playerWeapon;

    public void StartFiringPlayerWeapon()
    {
        if (!playerState.isDead && !playerState.isTraveling)
            playerWeapon.Fire();
    }


    public void StopFiringPlayerWeapon()
    {
        playerWeapon.CeaseFire();
    }

}
