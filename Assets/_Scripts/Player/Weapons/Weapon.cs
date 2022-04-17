using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    public ObjectPool projectileObjectPool;

    [SerializeField] private float timeBetweenShots;
    [SerializeField] private float fireDelay;
    [SerializeField] private Transform projectileSpawnTransform;
    private bool isFiring;
    private bool weaponIsActive;

    public UnityEvent OnWeaponFire;

    public void OnEnable()
    {
        isFiring = false;
        weaponIsActive = false;
    }

    public void Fire()
    {
        isFiring = true;
        if (!weaponIsActive)
        {
            weaponIsActive = true;
            StartCoroutine(OpenFire());
        }
    }

    public void CeaseFire()
    {
        isFiring = false;
    }

    IEnumerator OpenFire()
    {
        yield return new WaitForSeconds(fireDelay);
        while (isFiring)
        {
            SpawnProjectile();
            OnWeaponFire.Invoke();
            yield return new WaitForSeconds(timeBetweenShots);
        }
        weaponIsActive = false;
    }

    private void SpawnProjectile()
    {
        GameObject projectile = projectileObjectPool.Instantiate(projectileSpawnTransform.position, projectileSpawnTransform.rotation);
        ProjectileController projectileController = projectile.GetComponent<ProjectileController>();
        if (projectileController != null)
        {
            projectileController.OnSpawn();
        }
    }

}
