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
    public static PlayerData sharedInstance;
    
    public static float personalScoreOne;
    public static float personalScoreTwo;
    public static float personalScoreThree;

    public TMP_Text UIPlayerScoreOne;
    public TMP_Text UIPlayerScoreTwo;
    public TMP_Text UIPlayerScoreThree;
    
    public static float topHighscoreOne;
    public static float topHighscoreTwo; 
    public static float topHighscoreThree;
    
    public TMP_Text UITopScoreOne;
    public TMP_Text UITopScoreTwo;
    public TMP_Text UITopScoreThree;
    
    public static string userName = "{\"nickname\":\"RandomUser\"}";

    public TMP_Text nameInput;


    private void Awake()
    {
        // Singleton Pattern
        if (sharedInstance != null && sharedInstance != this)
        {
            Destroy(this.gameObject); // Destroy duplicate instance
            return;
        }
        sharedInstance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    
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
        Debug.Log("Changed Nickname!");
    }

    public void UpdateScores()
    {
        UIPlayerScoreOne.text = $"Your Score: {personalScoreOne.ToString()}";
        UIPlayerScoreTwo.text = $"Your Score: {personalScoreTwo.ToString()}";
        UIPlayerScoreThree.text = $"Your Score: {personalScoreThree.ToString()}";
        
        UITopScoreOne.text = $"Top Score: {topHighscoreOne.ToString()}";
        UITopScoreTwo.text = $"Top Score: {topHighscoreTwo.ToString()}";
        UITopScoreThree.text = $"Top Score: {topHighscoreThree.ToString()}";
    }
}
