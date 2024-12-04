using System;
using System.Collections.Generic;
using UnityEngine;
using BrainCloud;

namespace Backend
{
    public class Network : MonoBehaviour
    {
        private static BrainCloudWrapper bc;
        public static Network sharedInstance;
        private static GlobalData globalData;
        private static UserData userData;

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
        //Call on awake
        private static void LoadFromServer()
        {
            //Load Global Stats
            bc.GlobalStatisticsService.ReadAllGlobalStats((response, cbobject) =>
            {
                globalData.data = JsonUtility.FromJson<GlobalData.Data>(response);
                Debug.Log($"Json Converted! {globalData}");
            });

            bc.PlayerStatisticsService.ReadAllUserStats (responce, cbObject) =>
            {
                
            };
        }
        
        //Saving data, send to BrainCloud server
        public static void SaveData()
        {
            
        }
    }


    [Serializable]
    class GlobalData
    {
        public Data data;
        [Serializable]
        public class Data
        {
            public Statistics statistics;
            public int status;
        }
    
        [Serializable]
        public class Statistics
        {
            public float GlobalHighscoreSongThree;
            public float GlobalHighscoreSongOne;
            public float GlobalHighscoreSongTwo;
        }
    }

    [Serializable]
    class UserData
    {
        [Serializable]
        public class Data
        {
            public Statistics statistics;
            public int status;
        }

        [Serializable]
        public class Statistics
        {
            public float ScoreSongOne;
            public float ScoreSongThree;
            public float ScoreSongTwo;

        }
    }
    
    
}
