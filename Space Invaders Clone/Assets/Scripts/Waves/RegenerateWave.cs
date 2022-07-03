using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenerateWave : MonoBehaviour
{
    [SerializeField] private Transform NpcBattleField;
    [SerializeField] private SpawnEnemies spawnEnemies;

    public event Action ReturnAgentsToPool = delegate { };
    public event Action StopShooting = delegate { };

    public static RegenerateWave instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else 
        {
            Debug.LogError("Too many instances");
            Destroy(gameObject);
        }

    }

    public void StartInitializedWave()
    {
        InitializeWave();
        StartWave();
    }

    public void InitializeWave()
    {
        StopShooting();
        ReturnAgentsToPool();
        foreach (Transform line in NpcBattleField)
        {
            Destroy(line.gameObject);
        }
    }

    public void StartWave()
    {
        spawnEnemies.SpawnEnemyWave();
    }


}
