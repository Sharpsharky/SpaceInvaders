using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileLauncher : ProjectileLauncher
{
    private float fireRate = 1;
    private float nextFire = 0;
    [SerializeField] private PlayerConfigHolder playerConfigHolder;

    private void Awake()
    {
        fireRate = playerConfigHolder.ShipSettings.ShootingSpeed;
    }

    public void TryToShoot()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            ShootProjectile();
        }
    }
}
