using System;
using System.Collections.Generic;
using UnityEngine;
using BrainCloud;

namespace Backend
{
    //Global Data Handling
    public class Network : MonoBehaviour
    {
        private static BrainCloudWrapper bc;
        public static Network sharedInstance;
        private static Data data;
        

        public delegate void AuthenticationRequestCompleted();
        public delegate void AuthenticationRequestFailed();

        private void Awake()
        {
            // Singleton Pattern
            if (sharedInstance != null && sharedInstance != this)
            {
                Destroy(this.gameObject); // Destroy duplicate instance
                return;
            }
            sharedInstance = this;
            
            DontDestroyOnLoad(this.gameObject); // Make this GameObject persistent

            bc = FindObjectOfType<BrainCloudWrapper>();

            bc.Init();
            RequestAnonymousAuthentication();
            LoadFromServer();
        }

        void Update()
        {
            bc.RunCallbacks();
        }

        private void OnApplicationQuit()
        {
            throw new NotImplementedException();
        }

        #region Authntication Methods
        public bool IsAuthenticated() { return bc.Client.Authenticated; } // To validate authentication

        public void RequestAnonymousAuthentication(AuthenticationRequestCompleted authenticationRequestCompleted = null,
            AuthenticationRequestFailed authenticationRequestFailed = null)
        {
            // Success callback lambda
            BrainCloud.SuccessCallback successCallback = (responseData, cbobject) =>
            {
                Debug.Log($"RequestAnonymousAuthentication success: {responseData}");
                authenticationRequestCompleted?.Invoke();
            };

            // Failure callback lambda
            BrainCloud.FailureCallback failureCallback = (statusMessage, code, error, cbobject) =>
            {
                Debug.Log($"RequestAnonymousAuthentication failed: {statusMessage}");
                authenticationRequestFailed?.Invoke();
            };

            // BrainCloud request
            bc.AuthenticateAnonymous(successCallback, failureCallback);
        }
        #endregion
        
        //On successful response, load data from server
        ////Call on awake
        private void LoadFromServer()
        {
            //Load Global Stats
            GetUserStats();
        }

        #region Helper Methods
        
        // Read and convert User Statistics
        private void GetUserStats()
        {
            bc.PlayerStatisticsService.ReadAllUserStats((response, cbobject) =>
            {
                data = JsonUtility.FromJson<Data>(response);
                Debug.Log($"Json Converted! {data}");
            }); 
        }

        //Saving data, send to BrainCloud server
        public static void SaveData()
        {
            
        }
        
        //Update global statistics
        private void UpdateGlobalStat(string globalStatName, float globalStatValue)
        { 
            //TODO: Input Save GlobalStats here
            
            
        }

        #endregion
    }

    [Serializable]
    class Data
    {
        public Statistics statistics;
        public int status;
    }
    
    [Serializable]
    class Statistics
    {
        public float ScoreSongOne;
        public float ScoreSongThree;
        public float ScoreSongTwo;
    }
}
