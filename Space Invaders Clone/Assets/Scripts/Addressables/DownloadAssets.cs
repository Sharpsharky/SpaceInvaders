using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;

public class DownloadAssets : MonoBehaviour
{
    [SerializeField] private AssetReferenceTexture norAssetReference;
    [SerializeField] private AssetReferenceTexture metsmAssetReference;
    [SerializeField] private AssetReferenceTexture diffAssetReference;

    [SerializeField] private Material materialToChange;

    public static DownloadAssets instance;
    [SerializeField] TMP_Text addresable;

    public Material MaterialToChange { get => materialToChange;}

    private void Awake()
    {
        if(instance == null) instance = this;
        else
        {
            Debug.LogError("Too many instances");
            Destroy(gameObject);
        }

        Addressables.InitializeAsync().Completed += AssetsLoaded;
    }

    private void AssetsLoaded(AsyncOperationHandle<IResourceLocator> obj)
    {
        if (obj.Status != AsyncOperationStatus.Succeeded)
        {
            addresable.text = "Fail";
            return;
        }
        diffAssetReference.LoadAssetAsync<Texture>().Completed += (text) =>
        {
            
           MaterialToChange.SetTexture("_MainTex", diffAssetReference.Asset as Texture);

        };

        metsmAssetReference.LoadAssetAsync<Texture>().Completed += (text) =>
        {
            MaterialToChange.EnableKeyword("_METALLICGLOSSMAP");
           MaterialToChange.SetTexture("_MetallicGlossMap", metsmAssetReference.Asset as Texture);
        };

        norAssetReference.LoadAssetAsync<Texture>().Completed += (text) =>
        {
            MaterialToChange.EnableKeyword("_NORMALMAP");
            MaterialToChange.SetTexture("_BumpMap", norAssetReference.Asset as Texture);
        };

        //addresable.text = "Addressables: connected";
    }
}
