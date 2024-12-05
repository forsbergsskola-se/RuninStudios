using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Network = Backend.Network;

// Local Data Handling
public class PlayerData : MonoBehaviour
{
    public static float personalScoreOne;
    public static float personalScoreTwo;
    public static float personalScoreThree;

    public static float topHighscoreOne;
    public static float topHighscoreTwo; 
    public static float topHighscoreThree;
    
    public static string userName = "{\"nickname\":\"RandomUser\"}";

    public TMP_Text nameInput;
    // Compare current personal score with new
    // Compare new score with global score, then set new global Highscore if true
    // Call after finish a song
    public static void PostToLeaderboard(int songTrack, float score)
    {
        switch (songTrack)
        {
            case 0:
                Network.UploadToLeaderboard("SongOne", Convert.ToInt32(score), userName);
                break;
            case 1:
                Network.UploadToLeaderboard("SongTwo", Convert.ToInt32(score), userName);
                break;
            case 2:
                Network.UploadToLeaderboard("SongThree", Convert.ToInt32(score), userName);
                break;
        }
    }

    public void SetNickName()
    {
        userName = $"{{\"nickname\":\"{nameInput.text}\"}}";
    }

}
