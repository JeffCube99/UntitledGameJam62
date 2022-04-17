using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public void DestroyProjectile()
    {
        gameObject.SetActive(false);
    }
}
