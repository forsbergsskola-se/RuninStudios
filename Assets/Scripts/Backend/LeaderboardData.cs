using System;
using System.Collections.Generic;

namespace Backend
{
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
}