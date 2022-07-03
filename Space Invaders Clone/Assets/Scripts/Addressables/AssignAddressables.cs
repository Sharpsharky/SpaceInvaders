using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssignAddressables : MonoBehaviour
{

    [SerializeField] TMP_Text addressables;
    private readonly Dictionary<AssetReference, GameObject> spawnedReferences =
        new Dictionary<AssetReference, GameObject>();

    /// The Queue holds requests to spawn an instanced that were made while we are already loading the asset
    /// They are spawned once the addressable is loaded, in the order requested
    private readonly Dictionary<AssetReference, Queue<Vector3>> _queuedSpawnRequests =
        new Dictionary<AssetReference, Queue<Vector3>>();

    private readonly Dictionary<AssetReference, AsyncOperationHandle<GameObject>> _asyncOperationHandles =
        new Dictionary<AssetReference, AsyncOperationHandle<GameObject>>();

    public static AssignAddressables instance;

    [SerializeField] private List<AssetReference> gameObjectReferences;

    private void Awake()
    {
        if (instance == null) instance = this;
        else 
        {
            Debug.LogError("too many instances");
            Destroy(gameObject);
        }
    }


    public AssetReference Spawn(int index, Vector3 position, GameObjectPool gameObjectPool)
    {
        if (index < 0 || index >= gameObjectReferences.Count)
            return null;

        AssetReference assetReference = gameObjectReferences[index];

        if (assetReference.RuntimeKeyIsValid() == false)
        {
            Debug.Log("Invalid Key " + assetReference.RuntimeKey.ToString());
            return null;
        }

        if (_asyncOperationHandles.ContainsKey(assetReference))
        {
            if (_asyncOperationHandles[assetReference].IsDone)
                SpawnGameObjectFromLoadedReference(assetReference, position, gameObjectPool);
            else
                EnqueueSpawnForAfterInitialization(assetReference, position);

            return null;
        }

        LoadAndSpawn(assetReference, position, gameObjectPool   );

        return assetReference;
    }

    public GameObject GetGameObject(AssetReference assetReference)
    {
        return spawnedReferences[assetReference];
    }


    private void LoadAndSpawn(AssetReference assetReference, Vector3 position, GameObjectPool gameObjectPool)
    {
        var op = Addressables.LoadAssetAsync<GameObject>(assetReference);
        _asyncOperationHandles[assetReference] = op;
        op.Completed += (operation) =>
        {
            SpawnGameObjectFromLoadedReference(assetReference, position, gameObjectPool);
            if (_queuedSpawnRequests.ContainsKey(assetReference))
            {
                while (_queuedSpawnRequests[assetReference]?.Any() == true)
                {
                    var position = _queuedSpawnRequests[assetReference].Dequeue();
                    SpawnGameObjectFromLoadedReference(assetReference, position, gameObjectPool);
                }
            }
        };
    }

    private void EnqueueSpawnForAfterInitialization(AssetReference assetReference, Vector3 position)
    {
        if (_queuedSpawnRequests.ContainsKey(assetReference) == false)
            _queuedSpawnRequests[assetReference] = new Queue<Vector3>();
        _queuedSpawnRequests[assetReference].Enqueue(position);
    }

    private void SpawnGameObjectFromLoadedReference(AssetReference assetReference, Vector3 position, GameObjectPool gameObjectPool)
    {
        assetReference.InstantiateAsync(position, Quaternion.identity).Completed += (asyncOperationHandle) =>
        {
            if (!spawnedReferences.ContainsKey(assetReference))
            {
                spawnedReferences.Add(assetReference, asyncOperationHandle.Result);
            }
            asyncOperationHandle.Result.transform.parent = transform;
            gameObjectPool.AssignReference(asyncOperationHandle.Result);
            gameObjectPool.GenerateGameObjects();
            
            var notify = asyncOperationHandle.Result.AddComponent<NotifyOnDestroy>();
            notify.Destroyed += Remove;
            notify.AssetReference = assetReference;
            addressables.text = "Addressables success: success";

        };
    }

    private void Remove(AssetReference assetReference, NotifyOnDestroy obj)
    {
        Addressables.ReleaseInstance(obj.gameObject);

        spawnedReferences.Remove(assetReference);
        Debug.Log($"Removed all {assetReference.RuntimeKey.ToString()}");

        if (_asyncOperationHandles[assetReference].IsValid())
            Addressables.Release(_asyncOperationHandles[assetReference]);

        _asyncOperationHandles.Remove(assetReference);
    }
}