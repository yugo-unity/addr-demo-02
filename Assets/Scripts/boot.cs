using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class boot : MonoBehaviour
{
    [SerializeField]
    private bool debugDisableTutorial = false;
    [SerializeField]
    private int debugCoins = 0;
    [SerializeField]
    private int debugPremium = 0;

    // Start is called before the first frame update
    void Start()
    {
        PlayerData.DebugDisableTutorial = debugDisableTutorial;
        PlayerData.DebugCoins = debugCoins;
        PlayerData.DebugPremium = debugPremium;

        PlayerData.Create();

        AddressablesWrapper.instance.InitializeAsync().Completed += Handle_Completed;
    }

    private void Handle_Completed(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<UnityEngine.AddressableAssets.ResourceLocators.IResourceLocator> obj)
    {
        if(obj.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
        {
            AddressablesWrapper.instance.LoadSceneAsync("Start.unity");
        }
    }
}
