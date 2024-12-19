using System;

namespace Backend
{
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
}