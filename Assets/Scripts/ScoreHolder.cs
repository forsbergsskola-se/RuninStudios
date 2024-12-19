using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreHolder : MonoBehaviour
{
    
    public TMP_Text UIPlayerScoreOne;
    public TMP_Text UIPlayerScoreTwo;
    public TMP_Text UIPlayerScoreThree;
    
    public TMP_Text UITopScoreOne;
    public TMP_Text UITopScoreTwo;
    public TMP_Text UITopScoreThree;
    
    public void UpdateScores()
    {
        Debug.Log($"Player score: {PlayerData.personalScoreTwo}");
        Debug.Log($"Player score: {PlayerData.personalScoreTwo}");
        Debug.Log($"Player score: {PlayerData.personalScoreThree}");

        Debug.Log($"Top score: {PlayerData.topHighscoreOne}");
        Debug.Log($"Top score: {PlayerData.topHighscoreTwo}");
        Debug.Log($"Top score: {PlayerData.topHighscoreThree}");
        
        UIPlayerScoreOne.text = $"Your Score: {PlayerData.personalScoreOne.ToString()}";
        UIPlayerScoreTwo.text = $"Your Score: {PlayerData.personalScoreTwo.ToString()}";
        UIPlayerScoreThree.text = $"Your Score: {PlayerData.personalScoreThree.ToString()}";
        
        UITopScoreOne.text = $"Top Score: {PlayerData.topHighscoreOne.ToString()}";
        UITopScoreTwo.text = $"Top Score: {PlayerData.topHighscoreTwo.ToString()}";
        UITopScoreThree.text = $"Top Score: {PlayerData.topHighscoreThree.ToString()}";
    }

}
