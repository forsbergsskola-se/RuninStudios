using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Network = Backend.Network;

// Local Data Handling
public class PlayerData : MonoBehaviour
{
    private static string UserName;
    
    // Compare current personal score with new
    // Compare new score with global score, then set new global Highscore if true
    // Call after finish a song
    public static void PostToLeaderboard(int songTrack, float score)
    {
        switch (songTrack)
        {
            case 0:
                Network.UploadToLeaderboard("SongOne", Convert.ToInt32(score), UserName);
                break;
            case 1:
                Network.UploadToLeaderboard("SongTwo", Convert.ToInt32(score), UserName);
                break;
            case 2:
                Network.UploadToLeaderboard("SongThree", Convert.ToInt32(score), UserName);
                break;
        }
    }

}
