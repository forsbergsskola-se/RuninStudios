using System;
using System.Collections.Generic;
using UnityEngine;
using BrainCloud;

namespace Backend
{
    public class Network : MonoBehaviour
    {
        public static BrainCloudWrapper bc { get; private set; }
        public static Network sharedInstance;
        private static GlobalData globalData;
        private static UserData userData;
        private static LeaderboardData leaderboardIDOneData;
        private static LeaderboardData leaderboardIDTwoData;
        private static LeaderboardData leaderboardIDThreeData;

        private string leaderboardIDOne = "SongOne";
        private string leaderboardIDTwo = "SongTwo";
        private string leaderboardIDThree = "SongThree";
        BrainCloudSocialLeaderboard.SortOrder sortOrder = BrainCloudSocialLeaderboard.SortOrder.HIGH_TO_LOW;
        int startIndex = 0;
        int endIndex = 9;

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
            bc.Client.AuthenticationService.AuthenticateAnonymous(true, successCallback, failureCallback);
        }
        #endregion

        
        //On successful response, load data from server
        //Call on awake
        private void LoadFromServer()
        {
            //Load Global Stats
            ReadGlobalFromServer();
            
            //Load User Stats
            ReadUserFromServer();
            
            //Save Read data to local static script
            //LoadToLocal();
            
            // Read all leaderboards from server
            ReadAllLeaderboardFromServer();
            
            //Test posting
            UploadToLeaderboard(leaderboardIDOne, 10, "{\"nickname\":\"batman\"}");
        }

        //Get Global Data Statistics "Top Global HighScores"
        private void ReadGlobalFromServer()
        {
            //-----Error Handling-----
            SuccessCallback successCallback = (response, cbObject) =>
            {
                Debug.Log(string.Format("Success | {0}", response));
                globalData = JsonUtility.FromJson<GlobalData>(response);
            };
            FailureCallback failureCallback = (status, code, error, cbObject) =>
            {
                Debug.Log(string.Format("Failed | {0}  {1}  {2}", status, code, error));
            };
            bc.GlobalStatisticsService.ReadAllGlobalStats(successCallback, failureCallback);
        }

        //Get User Data Statistics "Top Personal HighScores"
        private void ReadUserFromServer()
        {
            //-----Error Handling-----
            SuccessCallback successCallback = (response, cbObject) =>
            {
                Debug.Log(string.Format("Success | {0}", response));
                userData = JsonUtility.FromJson<UserData>(response);
            };
            FailureCallback failureCallback = (status, code, error, cbObject) =>
            {
                Debug.Log(string.Format("Failed | {0}  {1}  {2}", status, code, error));
            };
            bc.PlayerStatisticsService.ReadAllUserStats(successCallback, failureCallback);
        }

        //Get Leaderboard Data
        private void ReadAllLeaderboardFromServer()
        {
            ReadLeaderboardID(leaderboardIDOne, leaderboardIDOneData);    
            ReadLeaderboardID(leaderboardIDTwo, leaderboardIDTwoData);            
            ReadLeaderboardID(leaderboardIDThree, leaderboardIDThreeData);
        }
        
        //Helper function
        private void ReadLeaderboardID(string leaderboardID, LeaderboardData leaderboardIDData)
        {
            SuccessCallback successCallback = (response, cbObject) =>
            {
                Debug.Log(string.Format("Success | {0}", response));
                leaderboardIDData = JsonUtility.FromJson<LeaderboardData>(response);
            };
            FailureCallback failureCallback = (status, code, error, cbObject) =>
            {
                Debug.Log(string.Format("Failed | {0}  {1}  {2}", status, code, error));
            };
            bc.LeaderboardService.GetGlobalLeaderboardPage(leaderboardID, sortOrder, startIndex, endIndex, successCallback, failureCallback);
        }

        private void UploadToLeaderboard(string leaderboardID, int score, string moi)
        {
            SuccessCallback successCallback = (response, cbObject) =>
            {
                Debug.Log(string.Format("Success | {0}", response));
            };
            FailureCallback failureCallback = (status, code, error, cbObject) =>
            {
                Debug.Log(string.Format("Failed | {0}  {1}  {2}", status, code, error));
            };

            bc.LeaderboardService.PostScoreToLeaderboard(leaderboardID, score, moi, successCallback, failureCallback);
        }
        
        //Saving data, send to BrainCloud server
        public static void SaveData()
        {
            
        }
        private static void LoadToLocal()
        {
            PlayerData.personalScoreOne = userData.data.statistics.ScoreSongOne;
            PlayerData.personalScoreTwo = userData.data.statistics.ScoreSongTwo;
            PlayerData.personalScoreThree = userData.data.statistics.ScoreSongThree;

            PlayerData.globalScoreOne = globalData.data.statistics.GlobalHighscoreSongOne;
            PlayerData.globalScoreTwo = globalData.data.statistics.GlobalHighscoreSongTwo;
            PlayerData.globalScoreThree = globalData.data.statistics.GlobalHighscoreSongThree;
        }
    }

    [Serializable]
    class LeaderboardData
    {
        public Data data;
        private int status;
        
        [Serializable]
        public class Data
        {
            public List<LeaderboardEntry> leaderboard;
            public bool moreBefore;
            public bool moreAfter;
            public long timeBeforeReset;
            public long server_time;
        }

        [Serializable]
        public class LeaderboardEntry
        {
            public string playerId;
            public int score;
            public string data;
            public long createdAt;
            public long updatedAt;
            public int index;
            public int rank;
            public string name;
            public string summaryFriendData;
            public string pictureUrl;
            public bool rewarded;
        }
    }


    [Serializable]
    class GlobalData
    {
        public Data data;
        private int statuse;
        
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
        public Data data;
        private int status;
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
