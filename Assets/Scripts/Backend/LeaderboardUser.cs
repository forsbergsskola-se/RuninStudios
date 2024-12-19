using System;

namespace Backend
{
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
}