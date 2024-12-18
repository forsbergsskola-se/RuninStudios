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
        private static LeaderboardUser leaderboardUser;
        private static LeaderboardData leaderboardOneData;
        private static LeaderboardData leaderboardTwoData;
        private static LeaderboardData leaderboardThreeData;

        private string leaderboardIDOne = "SongOne";
        private string leaderboardIDTwo = "SongTwo";
        private string leaderboardIDThree = "SongThree";
        BrainCloudSocialLeaderboard.SortOrder sortOrder = BrainCloudSocialLeaderboard.SortOrder.HIGH_TO_LOW;
        int startIndex = 0;
        int endIndex = 9;
        private int versionID = 1;

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
            
            // Read all leaderboards from server
            ReadAllLeaderboardFromServer();
            
            GetUserScoreInLeaderboard(leaderboardIDOne);

            GetUserScoreInLeaderboard(leaderboardIDTwo);

            GetUserScoreInLeaderboard(leaderboardIDThree);

        }

        //Get Global Data Statistics
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

        //Get User Data Statistics
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
            ReadLeaderboardID(leaderboardIDOne, leaderboardOneData);    
            ReadLeaderboardID(leaderboardIDTwo, leaderboardTwoData);            
            ReadLeaderboardID(leaderboardIDThree, leaderboardThreeData);
        }
        
        // Get HighScores in a leaderboard
        private void ReadLeaderboardID(string leaderboardID, LeaderboardData leaderboardIDData)
        {
            SuccessCallback successCallback = (response, cbObject) =>
            {
                Debug.Log(string.Format("Success | {0}", response));
                leaderboardIDData = JsonUtility.FromJson<LeaderboardData>(response);
                switch (leaderboardID)
                {
                    case "SongOne":
                        PlayerData.topHighscoreOne = leaderboardIDData.data.leaderboard[0].score;
                        break;
                    case "SongTwo":
                        PlayerData.topHighscoreTwo = leaderboardIDData.data.leaderboard[0].score;
                        break;
                    case "SongThree":
                        PlayerData.topHighscoreThree = leaderboardIDData.data.leaderboard[0].score;
                        break;
                }
                Debug.Log($"Top Score: {leaderboardIDData.data.leaderboard[0].score}");

            };
            FailureCallback failureCallback = (status, code, error, cbObject) =>
            {
                Debug.Log(string.Format("Failed | {0}  {1}  {2}", status, code, error));
            };
            bc.LeaderboardService.GetGlobalLeaderboardPage(leaderboardID, sortOrder, startIndex, endIndex, successCallback, failureCallback);
        }

        // Then assigning those values to local script, PlayerData.cs
        private void GetUserScoreInLeaderboard(string leaderboardID)
        {
            SuccessCallback successCallback = (response, cbObject) =>
            {
                Debug.Log(string.Format("Success | {0}", response));
                leaderboardUser = JsonUtility.FromJson<LeaderboardUser>(response);

                switch (leaderboardID)
                {
                    case "SongOne":
                        PlayerData.personalScoreOne = leaderboardUser.data.score.score;
                        break;
                    case "SongTwo":
                        PlayerData.personalScoreTwo = leaderboardUser.data.score.score;
                        break;
                    case "SongThree":
                        PlayerData.personalScoreThree = leaderboardUser.data.score.score;
                        break;
                }
                
                Debug.Log($"Your Score: {leaderboardUser.data.score.score}");
            };
            FailureCallback failureCallback = (status, code, error, cbObject) =>
            {
                Debug.Log(string.Format("Failed | {0}  {1}  {2}", status, code, error));
            };
            bc.LeaderboardService.GetPlayerScore(leaderboardID, versionID, successCallback, failureCallback);
        }
        
        //Update Leaderboard
        public static void UploadToLeaderboard(string leaderboardID, int score, string name)
        {
            SuccessCallback successCallback = (response, cbObject) =>
            {
                Debug.Log(string.Format("Success | {0}", response));
            };
            FailureCallback failureCallback = (status, code, error, cbObject) =>
            {
                Debug.Log(string.Format("Failed | {0}  {1}  {2}", status, code, error));
            };
            bc.LeaderboardService.PostScoreToLeaderboard(leaderboardID, score, name, successCallback, failureCallback);
        }
        
        //Update Global Statistics
        public static void IncrementGlobalStat(string globalName)
        {
            string statistics = $"{{\"{globalName}\":1}}";
            SuccessCallback successCallback = (response, cbObject) =>
            {
                Debug.Log(string.Format("Success | {0}", response));
            };
            FailureCallback failureCallback = (status, code, error, cbObject) =>
            {
                Debug.Log(string.Format("Failed | {0}  {1}  {2}", status, code, error));
            };

            bc.GlobalStatisticsService.IncrementGlobalStats(statistics, successCallback, failureCallback);
        }
    }

    #region Leaderboard Class
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

    #endregion

    #region LeaderboardUser Class
    [Serializable]
    public class LeaderboardUser
    {
        public Data data; // Contains the main data object
        public int status; // Represents the HTTP status code

        [Serializable]
        public class Data
        {
            public Score score; // Contains score-related information
        }

        [Serializable]
        public class Score
        {
            public float score; // The player's score
            public Metadata data; // Additional data (like nickname)
            public long createdAt; // Timestamp when the score was created
            public long updatedAt; // Timestamp when the score was last updated
            public string leaderboardId; // The ID of the leaderboard
            public int versionId; // The version ID of the leaderboard
        }

        [Serializable]
        public class Metadata
        {
            public string nickname; // Additional metadata (e.g., nickname)
        }
    }
    #endregion

    #region GlobalData Class
    [Serializable]
    class GlobalData
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
            public float SongOneFailures;
            public float SongOneSuccesses;
            public float SongThreeFailures;
            public float SongThreeSuccesses;
            public float SongTwoFailures;
            public float SongTwoSuccesses;
        }
    }
    #endregion

    #region UserData Class
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
    #endregion

    
}
