using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class SongManger2 : MonoBehaviour
{
    public AudioSource audioSource;
    [SerializeField] private NoteGiver noteGiver;
    public int missedNotesCount = 0;
    [SerializeField] private int maxMissedNotes = 5;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject gameWinningUI;
    
    public TextAsset csvFile;

    private List<CsvDataEntry> dataEntries = new List<CsvDataEntry>();

    private void Start()
    {
        audioSource.clip = SongHolder.songToPlay;
        csvFile = SongHolder.csvToRead;
    }

    public void SongStart()
    {
        noteGiver.SettingPool();
        ScoreManager.ResetStats();
        audioSource.Play();
        Invoke("TriggerGameFinish", audioSource.clip.length + 5);
    }

    public void StartNoteSpawns()
    {
        if (csvFile != null)
        {
            LoadCsv(csvFile);
            ScheduleFunctions();
        }
        else
        {
            Debug.LogError("CSV file not assigned!");
        }
    }

    // Function to parse the CSV file
    void LoadCsv(TextAsset file)
    {
        string[] rows = file.text.Split('\n');
        HashSet<string> uniqueEntries = new HashSet<string>(); // Track unique entries

        foreach (string row in rows)
        {
            if (uniqueEntries.Contains(row)) continue; // Skip duplicates

            uniqueEntries.Add(row); // Add to the set
            string[] columns = row.Split(',');

            if (columns.Length >= 2 && int.TryParse(columns[0], out int timestamp) && int.TryParse(columns[1], out int functionID))
            {
                dataEntries.Add(new CsvDataEntry { Timestamp = timestamp, FunctionID = functionID });
            }
        }
    }
    
    // Schedule function calls based on the parsed data
    void ScheduleFunctions()
    {
        foreach (var entry in dataEntries)
        {
            // Schedule function calls using Invoke
            Invoke($"Function_{entry.FunctionID}", entry.Timestamp / 1000f);
        }
    }
    // Function calls for different color ID
    void Function_0() { noteGiver.SpawnNote(0); }
    void Function_1() { noteGiver.SpawnNote(1); }
    void Function_2() { noteGiver.SpawnNote(2); }
    void Function_3() { noteGiver.SpawnNote(3); }

    public void NoteMissed()
    {
        missedNotesCount++;
        Debug.Log("Missed Notes Count called");
        if (missedNotesCount >= maxMissedNotes)
        {
            TriggerGameOver();
        }
    }

    private void TriggerGameFinish()
    {
        Debug.Log("Song Finished?");
        StopAllCoroutines();
        gameWinningUI.SetActive(true);
        SendScore();
    }
    private void TriggerGameOver()
    {
        Debug.Log("TriggerGameOver() called!");
        audioSource.Stop();
        StopAllCoroutines();
        gameOverUI.SetActive(true);
        Debug.Log("Game over");
        SendScore();
    }
    
    //Call post-song
    private void SendScore() { PlayerData.PostToLeaderboard(SongHolder.songTrack, ScoreManager.score); }
    

    // Simple class to store CSV data
    private class CsvDataEntry
    {
        public int Timestamp; // Time in milliseconds
        public int FunctionID; // Function ID (0, 1, or 2)
    }
}