using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{

    public static AdsManager instance;    

    [SerializeField] private string androidGameId;
    [SerializeField] private string iOSGameId;
    [SerializeField] private bool testMode = true;

    [SerializeField] private string androidAdUnitId;
    [SerializeField] private string iOSAdUnitId;
    private string gameId;
    private string adUnitId;


    private void Awake() {
        if(instance != null && instance != this){
            Destroy(gameObject);
        }
        else{
            InitiliseAds();
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void InitiliseAds()
    {
#if UNITY_IOS

        gameId = iOSGameId;
        adUnitId = iOSAdUnitId;

#elif UNITY_ANDROID

        gameId = androidGameId;
        adUnitId = androidAdUnitId;

#elif UNITY_EDITOR

        gameId = androidGameId;
        adUnitId = androidAdUnitId;

#endif
        if(!Advertisement.isInitialized){
            Advertisement.Initialize(gameId,testMode,this);
        }
       
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads Initialization Complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Advertisement.Show(placementId,this);
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if(placementId.Equals(adUnitId) /*&& showCompletionState.Equals(UnityAdsCompletionState.COMPLETED)*/){
            Debug.Log("CONTINUE FUNCIONA");
            //CONTINUAR JUEGO
            GameManager.instance.PostAds();
            //GameManager.instance.ReloadLevel();
        }
    }

    public void ShowAd()
    {        
        Advertisement.Load(adUnitId,this);
    }

    
}
