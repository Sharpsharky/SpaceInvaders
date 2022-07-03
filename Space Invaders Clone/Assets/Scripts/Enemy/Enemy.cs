using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IGameObjectPooled, IDemageble
{
    [SerializeField]
    private EnemyConfig enemyConfig;

    private int currentLifes;
    private GameObjectPool pool;
    private EnemyInput enemyInput;
    private EnemyWaveMovement enemyWaveMovement;

    public static event Action OnEnemyKilled;
    public event Action OnEnemyGetScore = delegate { };

    public GameObjectPool Pool
    {
        get { return pool; }
        set
        {
            if (pool == null)
                pool = value;
            else
                throw new System.Exception("Bad pool");
        }
    }


    private void Awake()
    {
        enemyInput = GetComponent<EnemyInput>();
    }

    private void Start()
    {
        RegenerateWave.instance.ReturnAgentsToPool += OnReturnToPool;
    }

    public void OnEnable()
    {
        currentLifes = enemyConfig.Hp;
    }

    public void DealDamage(int damageToDeal)
    {
        currentLifes -= damageToDeal;
        if(currentLifes <= 0)
        {
            OnEnemyGetScore();
            OnEnemyKilled();
            OnReturnToPool();
            EnemyShooting.instance.RemoveEnemyFromHashSet(enemyInput);
        }
    }

    private void OnReturnToPool()
    {
        pool.ReturnToPool(gameObject);
    }


}
