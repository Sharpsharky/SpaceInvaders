using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncherEnemy : ProjectileLauncher
{

    private void Awake()
    {
        GetComponent<IEntetyImputable>().OnFire += ShootProjectile;

    }

    public void SetPool(GameObjectPool gameObjectPool)
    {
        projectilePool = gameObjectPool;
    }
}
