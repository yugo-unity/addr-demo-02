using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets.ResourceLocators;

public class AddressablesWrapper : MonoBehaviour
{
    public static AddressablesWrapper instance = null;

    private System.Diagnostics.Stopwatch sw;
    private System.Diagnostics.Stopwatch swAsset;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);

            sw = new System.Diagnostics.Stopwatch();
            swAsset = new System.Diagnostics.Stopwatch();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public AsyncOperationHandle<IResourceLocator> InitializeAsync()
    {
        sw.Start();
        var handle = Addressables.InitializeAsync();
        handle.Completed += InitializeAsync_Completed;
        return handle;
    }

    private void InitializeAsync_Completed(AsyncOperationHandle<IResourceLocator> obj)
    {
        sw.Stop();
        Debug.Log($"Addressalbes.InitializeAsync completed. {sw.Elapsed}");
        sw.Reset();
    }

    public AsyncOperationHandle<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance> LoadSceneAsync(string sceneName)
    {
        return LoadSceneAsync(sceneName, UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

    public AsyncOperationHandle<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance> LoadSceneAsync(string sceneName, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        sw.Start();
        var handle = Addressables.LoadSceneAsync(sceneName, mode);
        handle.Completed += LoadSceneAsync_Completed;
        return handle;
    }

    private void LoadSceneAsync_Completed(AsyncOperationHandle<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance> obj)
    {
        sw.Stop();
        Debug.Log($"Addressalbes.LoadSceneAsync completed. {sw.Elapsed}");
        sw.Reset();

        Debug.Log($"Profiler.GetTotalReservedMemoryLong: {UnityEngine.Profiling.Profiler.GetTotalReservedMemoryLong()}");
        Debug.Log($"Profiler.GetTotalAllocatedMemoryLong: {UnityEngine.Profiling.Profiler.GetTotalAllocatedMemoryLong()}");
        Debug.Log($"Profiler.GetTotalUnusedReservedMemoryLong: {UnityEngine.Profiling.Profiler.GetTotalUnusedReservedMemoryLong()}");
    }

    public AsyncOperationHandle<TObject> LoadAssetAsync<TObject>(object key)
    {
        Debug.Log($"Addressables.LoadAssetAsync: {key.ToString()}");
        swAsset.Start();
        var handle = Addressables.LoadAssetAsync<TObject>(key);
        handle.Completed += LoadAssetAsync_Completed;
        return handle;
    }

    private void LoadAssetAsync_Completed<TObject>(AsyncOperationHandle<TObject> obj)
    {
        swAsset.Stop();
        Debug.Log($"Addressalbes.LoadAssetAsync completed. {sw.Elapsed}");
        swAsset.Reset();
    }
}
