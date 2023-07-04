using System.Linq;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.EventSystems;

namespace BreakingRat.Infrastructure.Services.Ads
{
    public class AdsService :
        IAdsService,
        IUnityAdsInitializationListener,
        IUnityAdsLoadListener,
        IUnityAdsShowListener
    {
        private const string AndroidId = "5289617";
        private const string IOSId = "5289616";

        private const string AndroidAdId = "Interstitial_Android";
        private const string IOSAdId = "Interstitial_iOS";

        private bool _initialized = false;
        private string _gameID;
        private string _adID;
        private bool _testMode = false;

        public AdsService()
        {
            Initialize();
        }

        public void Initialize()
        {
            _initialized = true;

            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    _gameID = AndroidId;
                    _adID = AndroidAdId;
                    break;
                case RuntimePlatform.IPhonePlayer:
                    _gameID = IOSId;
                    _adID = IOSAdId;
                    break;
                case RuntimePlatform.WindowsEditor:
                    _gameID = AndroidId;
                    _adID = AndroidAdId;
                    break;
                default:
                    Debug.Log("Unsupported platform for ads");
                    break;
            }

            Advertisement.Initialize(_gameID, _testMode, this);
        }

        public void ShowAd()
        {
            if (_initialized == false)
                Initialize();

            Debug.Log("Showing Ad: " + _gameID);
            Advertisement.Show(_adID, this);
            LoadAd();
        }

        private void LoadAd()
        {
            Debug.Log("Loading Ad: " + _gameID);
            Advertisement.Load(_adID, this);
        }

        public void OnInitializationComplete()
        {
            Debug.Log("Unity Ads initialization complete.");

            var eventSystems = GameObject.FindObjectsOfType<EventSystem>();

            if (eventSystems.Length > 1)
            {
                for (int i = 1; i < eventSystems.Length; i++)
                {
                    GameObject.Destroy(eventSystems[i].gameObject);
                }
            }
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
        }

        public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
        {
            Debug.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
        }

        public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
        {
            Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        }

        public void OnUnityAdsAdLoaded(string adUnitId)
        {
        }

        public void OnUnityAdsShowStart(string adUnitId)
        {
        }

        public void OnUnityAdsShowClick(string adUnitId)
        {
        }

        public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
        {
        }
    }
}
