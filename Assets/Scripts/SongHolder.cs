using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongHolder : MonoBehaviour
{
    public static SongHolder instance;
    
    public static AudioClip songToPlay;
    public static TextAsset csvToRead;
    public static int songTrack; // Network purpose: to track which song is being played
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance exists
        }
    }
}
