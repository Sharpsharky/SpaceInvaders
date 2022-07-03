using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressablesManager : MonoBehaviour
{
    [SerializeField] private AssetReference npc_1;

    private void Start()
    {
        Addressables.InitializeAsync().Completed += AddressablesManagerCompleted;
    }

    private void AddressablesManagerCompleted(AsyncOperationHandle<IResourceLocator> obj)
    {
        npc_1.InstantiateAsync().Completed += (go) =>
         {
             GameObject playerController = go.Result;
         };
    }
}
