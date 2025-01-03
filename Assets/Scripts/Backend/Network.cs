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


        private readonly string leaderboardIDOne = "SongOne";
        private readonly string leaderboardIDTwo = "SongTwo";
        private readonly string leaderboardIDThree = "SongThree";
        readonly BrainCloudSocialLeaderboard.SortOrder sortOrder = BrainCloudSocialLeaderboard.SortOrder.HIGH_TO_LOW;
        readonly int startIndex = 0;
        readonly int endIndex = 9;
        private readonly int versionID = 1;

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

        
        // On successful response, load data from server
        // Call on awake
        private void LoadFromServer()
        {
            // Load Global Stats
            ReadGlobalFromServer();
            
            // Load User Stats
            ReadUserFromServer();
            
            // Read all leaderboards from server
            ReadAllLeaderboardFromServer();

            // Read all user scores in each leaderboard
            ReadAllUserStats();

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
        public void ReadAllLeaderboardFromServer()
        {
            ReadLeaderboardID(leaderboardIDOne, leaderboardOneData);    
            ReadLeaderboardID(leaderboardIDTwo, leaderboardTwoData);            
            ReadLeaderboardID(leaderboardIDThree, leaderboardThreeData);
        }
        
        // Get Leaderboard Data
        public void ReadAllUserStats()
        {
            GetUserScoreInLeaderboard(leaderboardIDOne);
            GetUserScoreInLeaderboard(leaderboardIDTwo);
            GetUserScoreInLeaderboard(leaderboardIDThree);
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
        public void GetUserScoreInLeaderboard(string leaderboardID)
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
}
