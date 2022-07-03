using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType { NPC1, NPC2, NPC3 }


public class SpawnEnemies : MonoBehaviour
{
    [SerializeField]
    private GameObject npc1, npc2, npc3;
    [SerializeField]
    private Transform npcBattlefield;
    [SerializeField]
    private List<EnemyType> enemySpawningQueue = new List<EnemyType>();
    [SerializeField]
    private int enemiesInLine = 11;
    [SerializeField]
    private float spaceBetweenEnemies = 2.5f; 
    [SerializeField]
    private float firstLineZPos = 4f; 
    [SerializeField]
    private float spaceBetweenLines = 3f; 
    //[SerializeField] private float lineSpawningOffset = 0.5f; //const

    [Space]
    [Header("Spawning Points")]
    [SerializeField]
    float spawningAxis = 35;
    [SerializeField]
    float upperCorner = 12;
    [SerializeField]
    private GameObject enemyLine;

    private EnemyShooting enemyShooting;
    
    [Space]
    [Header("Object Pooling")]
    [SerializeField]
    private GameObjectPool npc1Pool;
    [SerializeField]
    private GameObjectPool npc2Pool;
    [SerializeField]
    private GameObjectPool npc3Pool;

    [SerializeField] GameObjectPool projectilePool;

    private float lineOffset = 0;


    private Dictionary<EnemyType, GameObject> enemiesDictionary = new Dictionary<EnemyType, GameObject>();
    private Dictionary<EnemyType, GameObjectPool> enemiesPool = new Dictionary<EnemyType, GameObjectPool>();
    
    void Awake()
    {
        enemyShooting = GetComponent<EnemyShooting>();
        ConnectEnemyTypesToGameObjects();
        CreatePools();
    }

    public void SpawnEnemyWave()
    {
        for (int i = 0; i < enemySpawningQueue.Count; i++)
        {
            SpawnEnemyLine(firstLineZPos - i * spaceBetweenLines, enemySpawningQueue[i], i);
        }

        enemyShooting.StartShooting();
    }
    private void SpawnEnemyLine(float zPosOfLine, EnemyType enemyType, int currentLineCount)
    {
        GameObject targetPoints = InitializeTargetPoints();

        for (int i = 0; i < enemiesInLine; i++)
        {
            
            GameObject newEnemy = SpawnEnemy(enemyType, DrawPositionToSpawn());
            EnemyInitialMovement enemyInitialMovement = newEnemy.GetComponent<EnemyInitialMovement>();

            GameObject targetPoint = new GameObject("TargetPoint");
            targetPoint.transform.parent = targetPoints.transform;
            targetPoint.transform.localPosition = new Vector3(i * spaceBetweenEnemies, 0, zPosOfLine);

            enemyInitialMovement.TargetPosition = targetPoint.transform;
            if (!newEnemy.activeSelf)
            {
                Debug.LogError("Activating");
                newEnemy.SetActive(true);
            }
        }

        //Setting the position of the line
        float x0 = targetPoints.transform.GetChild(0).position.x;
        float x1 = targetPoints.transform.GetChild(enemiesInLine - 1).position.x;
        float middle = (x0 + x1) / 2;
        targetPoints.transform.position = new Vector3(-middle, 0);
        targetPoints.transform.parent = npcBattlefield;

        lineOffset = -middle;
    }

    private Vector3 DrawPositionToSpawn()
    {
        float axis;
        int rand = Random.Range(0, 2);
        if(rand == 0) axis = spawningAxis;
        else axis = -spawningAxis;

        float zRand = Random.Range(-upperCorner, -2*upperCorner);

        return new Vector3(axis, 0, zRand);

    }
    private GameObject InitializeTargetPoints()
    {
        GameObject targetPointsParent = Instantiate(enemyLine, new Vector3(0, 0, 0), Quaternion.identity);
        targetPointsParent.transform.position = new Vector3(0, 0, 0);

        return targetPointsParent;
    }

    private void CreatePools()
    {
        enemiesPool.Add(EnemyType.NPC1, npc1Pool);
        enemiesPool.Add(EnemyType.NPC2, npc2Pool);
        enemiesPool.Add(EnemyType.NPC3, npc3Pool);
    }

    private void ConnectEnemyTypesToGameObjects()
    {
        enemiesDictionary.Add(EnemyType.NPC1, npc1);
        enemiesDictionary.Add(EnemyType.NPC2, npc2);
        enemiesDictionary.Add(EnemyType.NPC3, npc3);
    }

    private GameObject GetEnemyFromPool(EnemyType enemyType)
    {
        return enemiesPool[enemyType].Get();
    }
    private GameObject SpawnEnemy(EnemyType enemyType, Vector3 positionToSpawn)
    {

        GameObject enemyToSpawn = GetEnemyFromPool(enemyType);
        if (enemyToSpawn.transform.parent != null) Debug.LogError("It should not be in parent");
        enemyToSpawn.transform.position = positionToSpawn;
        
        enemyToSpawn.GetComponent<ProjectileLauncherEnemy>().SetPool(projectilePool);
        enemyToSpawn.GetComponent<EnemyInitialMovement>().StartPosition = positionToSpawn;
        return enemyToSpawn;
    }


}

