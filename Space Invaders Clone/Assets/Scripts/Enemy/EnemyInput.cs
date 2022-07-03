using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInput : MonoBehaviour, IEntetyImputable
{
    public event Action OnFire = delegate { };

    private void OnEnable()
    {
        AddEnemyInputToHashSet();
    }

    private void AddEnemyInputToHashSet()
    {
        EnemyShooting.instance.EnemyInputs.Add(this);
    }

    public void Shoot()
    {
        OnFire();
    }


}
