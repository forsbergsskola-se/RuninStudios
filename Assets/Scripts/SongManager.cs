using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManager : MonoBehaviour
{
    public AudioSource audioSource;
    float songPosition;//the current position of the song (in seconds)
    float songPosInBeats;//the current position of the song (in beats)
    float secPerBeat; //the duration of a beat
    float dsptimesong; // Time (in seconds) when the song started
    [SerializeField] float bpm = 86f; // Beats per minute of the song
    [SerializeField] float[] notes; // timing info in terms of the beat of the song
    int nextIndex = 0; // Index of the next note to be spawned
    [SerializeField] private int maxMissedNotes = 3;
    private int missedNotesCount = 0;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private NoteGiver noteGiver;
    
    private void Start()
    {
        noteGiver.SettingPool();

        secPerBeat = bpm / 60f; //calculated seconds per beat
        dsptimesong = (float)AudioSettings.dspTime; //record the start time
        audioSource.Play();

        var totalNotes = secPerBeat * audioSource.clip.length;
        notes = new float[(int)totalNotes];
        Debug.Log("Song started");
        StartCoroutine(RhythmicTrigger());
    }
    
    private IEnumerator RhythmicTrigger()
    {
        while (true)
        {
            songPosition = (float)(AudioSettings.dspTime - dsptimesong); // ensures precise timing
            songPosInBeats = songPosition / secPerBeat;

            if (nextIndex < notes.Length && notes[nextIndex] < songPosInBeats)
            {
                noteGiver.SpawnNote();
                nextIndex++;
            }

            // Calculate wait time to stay in rhythm
            float nextBeatTime = secPerBeat - (songPosition % secPerBeat);
            yield return new WaitForSeconds(nextBeatTime);
        }
    }

    public void NoteMissed()
    {
        missedNotesCount++;
        Debug.Log("Missed Notes Count called");
        if (missedNotesCount >= maxMissedNotes)
        {
            TriggerGameOver();
        }
    }

    private void TriggerGameOver()
    {
        Debug.Log("TriggerGameOver() called!");
        audioSource.Stop();
        StopAllCoroutines();
        gameOverUI.SetActive(true);
        Debug.Log("Game over");
    }
}
