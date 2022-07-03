using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class GameObjectPool : MonoBehaviour
{
    [SerializeField] private int indexOfGameObjectInAddressables;
    [SerializeField] private int countToSpawnOnInit = 50;

    private GameObject gameObjectReference;
    private AssetReference assetReference;
    private Queue<GameObject> gameObjects = new Queue<GameObject>();


    private void Awake()
    {
    }

    private void Start()
    {
        GenerateAddressableGameObject();
        //GetAddressableReference();
        //AddGameObject(countToSpawnOnInit);
        
    }


    public GameObject Get()
    {
        if (gameObjects.Count == 0)
        {
            AddGameObject(1);
        }
        GameObject gameObj = gameObjects.Dequeue();
        gameObj.transform.parent = null;
        gameObj.SetActive(true);
        return gameObj;
    }

    public void ReturnToPool(GameObject gameObj)
    {
        gameObj.SetActive(false);
        gameObj.transform.position = new Vector3(0, 0, 0);
        gameObj.transform.parent = transform;
        gameObjects.Enqueue(gameObj);
    }

    private void AddGameObject(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject gameObj = Instantiate(gameObjectReference);
            gameObj.SetActive(false);
            gameObj.transform.parent = transform;
            gameObj.GetComponent<IGameObjectPooled>().Pool = this;
            gameObjects.Enqueue(gameObj);

        }
    }

    public void AssignReference(GameObject refe)
    {
        gameObjectReference = refe;
    }

    public void GenerateGameObjects()
    {
        AddGameObject(countToSpawnOnInit);
    }

    private void GenerateAddressableGameObject() 
    {

        assetReference = AssignAddressables.instance.Spawn(indexOfGameObjectInAddressables, new Vector3(0, 0, 0),this);
    }

}

internal interface IGameObjectPooled
{
    GameObjectPool Pool { get; set; }
}