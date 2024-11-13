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
    [SerializeField] private NoteGiver noteGiver;
    
    // Start is called before the first frame update
    private void Start()
    {
        secPerBeat = 60f / bpm; //calculated secunds per beat
        dsptimesong = (float)AudioSettings.dspTime; //record the start time
        audioSource.Play();
        Debug.Log("Song started");
    }

    // Update is called once per frame
    void Update()
    {
        songPosition = (float)(AudioSettings.dspTime - dsptimesong);//ensures precise timing
        songPosInBeats = songPosition / secPerBeat;
        
        if (nextIndex < notes.Length && notes [nextIndex] < songPosInBeats)
        {
            noteGiver.SpawnNote();
            nextIndex++;
        }
    }
    
    
}
