using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Local Data Handling
public class PlayerData : MonoBehaviour
{
    public static float personalScoreOne;
    public static float personalScoreTwo;
    public static float personalScoreThree;

    public static float globalScoreOne;
    public static float globalScoreTwo; 
    public static float globalScoreThree;
    
    private static string UserName;

    // Compare current personal score with new
    // Compare new score with global score, then set new global Highscore if true
    // Call after finish a song
    public static void SetPersonalScore(int songTrack, float score)
    {
        switch (songTrack)
        {
            case 1:
                if (personalScoreOne > score) return;
                personalScoreOne = score;
                if (ValidateScores(globalScoreOne, personalScoreOne)) { globalScoreOne = personalScoreOne; }
                break;
            case 2:
                if (personalScoreTwo > score) return;
                personalScoreTwo = score;
                if (ValidateScores(globalScoreTwo, personalScoreTwo)) { globalScoreTwo = personalScoreTwo; }
                break;
            case 3:
                if (personalScoreThree > score) return;
                personalScoreThree = score;
                if (ValidateScores(globalScoreThree, personalScoreThree)) { globalScoreThree = personalScoreThree; }
                break;
        }
    }

    //Compare the global score with personal score
    private static bool ValidateScores(float globalScore, float personalScore)
    {
        return personalScore > globalScore;
    }
}
