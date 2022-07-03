using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IGameObjectPooled
{
    [SerializeField] protected ProjectileConfig projectileConfig;

    [SerializeField] private string objectTagToKill = "Enemy";

    private float currentLifeTime = 0;
    private GameObjectPool pool;
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

    void OnEnable()
    {
        currentLifeTime = 0;
    }

    public void DestroyGameObj()
    {
        pool.ReturnToPool(gameObject);
    }

    private void Update()
    {
        transform.Translate(-Vector3.forward * projectileConfig.MoveSpeed * Time.deltaTime);
        currentLifeTime += Time.deltaTime;
        if (currentLifeTime >= projectileConfig.LivingTime) DestroyGameObj();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == objectTagToKill)
        {
            other.gameObject.GetComponent<IDemageble>().DealDamage(1);
            DestroyGameObj();
        }
    }

    private void AddScore()
    {

    }

}
