using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class SoundtrackManager : MonoBehaviour
{
    /*[EventRef] public string soundTrackEvent = "event:/1730736442085ioqbhouk";
    private EventInstance soundTrackInstance;

    public NoteGiver noteGiver;
    [SerializeField] private Note notePrefab;

    void Start()
    {
        // Create the event instance and start it
        soundTrackInstance = RuntimeManager.CreateInstance(soundTrackEvent);

        // Assign the callback function to the instance
        soundTrackInstance.setCallback((OnSoundtrackEventCallback), EVENT_CALLBACK_TYPE.TIMELINE_MARKER);

        // Start the event
        soundTrackInstance.start();
    }

    private FMOD.RESULT OnSoundtrackEventCallback(EVENT_CALLBACK_TYPE type, IntPtr intPtr, IntPtr parameterPtr)
    {
        // Check if this callback is for a marker
        if (type == EVENT_CALLBACK_TYPE.TIMELINE_MARKER)
        {
            var marker = (TIMELINE_MARKER_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_MARKER_PROPERTIES));

            Debug.Log("Marker reached: " + marker.name);

            // Call functions based on marker names
            switch (marker.name)
            {
                case "Red Note":
                    noteGiver.SendNote(notePrefab, "RED");
                    break; 
                case "Blue Note":
                    noteGiver.SendNote(notePrefab, "BLUE");
                    break;
                case "Green Note":
                    noteGiver.SendNote(notePrefab, "GREEN");
                    break;
            }
        }
        return FMOD.RESULT.OK;
    }
    
    void OnDestroy()
    {
        // Stop the soundtrack instance and release it
        soundTrackInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        soundTrackInstance.release();
    }*/
}
