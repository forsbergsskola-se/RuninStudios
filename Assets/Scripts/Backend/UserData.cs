using System;

namespace Backend
{
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