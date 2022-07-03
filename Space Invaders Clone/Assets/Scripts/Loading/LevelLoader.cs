using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private string levelName = "Arcade";


    private void Start()
    {
        //LoadLevel(1);
        StartCoroutine(LoadAddressableLevel(levelName));
    }
    private void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsync(sceneIndex));
    }

    private IEnumerator LoadAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            Debug.Log(progress);
            yield return null;
        }

    }

    private IEnumerator LoadAddressableLevel(string addressableKey)
    {

        AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(addressableKey, LoadSceneMode.Additive);

        while (!handle.IsDone)
        {
            float progress = handle.PercentComplete;
            slider.value = progress;
            Debug.Log(progress);
            yield return null;
        }


        //Debug.LogError("0 index scene is: " + SceneManager.GetSceneByBuildIndex(0).name);
        AsyncOperation operation = SceneManager.UnloadSceneAsync(0);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            Debug.Log(progress);
            yield return null;
        }



    }

}

