using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
public class ScoreManager : MonoBehaviour
{
    public static float score; // Refer to UI Score
    private static int multiplier = 1; // Refer to UI Multiplier
    private static int defaultMultiplier = 1;
    private static int maxMultiplier = 8;
    public static int successStreak; // Refer to UI Multiplier Count Up

    public TMP_Text scoreText;
    public TMP_Text multiplierText;
    public TMP_Text multiplierCountUpText;
    public void AddScore(float plusScore) // Call on HIT notes
    {
        score += plusScore * multiplier;
        scoreText.text = score.ToString();
        NoteSuccessCounter();
    }
    public void ResetMultiplier() // Call on MISSED notes
    {
        multiplier = defaultMultiplier;
        multiplierText.text = multiplier.ToString();
        successStreak = 0;
        multiplierCountUpText.text = successStreak.ToString();
    }

    private void NoteSuccessCounter()
    {
        successStreak++;
        multiplierCountUpText.text = successStreak.ToString();
        if (successStreak % 5 == 0) IncreaseMultiplier();
    }
    private void IncreaseMultiplier() // In Successful streak, increase multiplier
    {
        if (multiplier != maxMultiplier)
        {
            multiplier *= 2;
            multiplierText.text = multiplier.ToString();
        }
    }

    public static void ResetStats()
    {
        score = 0;
        multiplier = 1;
        successStreak = 0; // Refer to UI Multiplier Count Up
    }
}
