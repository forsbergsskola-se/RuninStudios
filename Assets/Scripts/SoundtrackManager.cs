using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class SoundtrackManager : MonoBehaviour
{
    [EventRef] public string soundTrackEvent = "event:/1730736442085ioqbhouk";

    private EventInstance soundTrackInstance;

    void Start()
    {
        // Create the event instance and start it
        soundTrackInstance = RuntimeManager.CreateInstance(soundTrackEvent);

        // Assign the callback function to the instance
        soundTrackInstance.setCallback(new FMOD.Studio.EVENT_CALLBACK(OnSoundtrackEventCallback), FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER);

        // Start the event
        soundTrackInstance.start();
    }

    private static FMOD.RESULT OnSoundtrackEventCallback(FMOD.Studio.EVENT_CALLBACK_TYPE type, IntPtr intPtr, IntPtr parameterPtr)
    {
        // Check if this callback is for a marker
        if (type == FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER)
        {
            var marker = (FMOD.Studio.TIMELINE_MARKER_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_MARKER_PROPERTIES));

            Debug.Log("Marker reached: " + marker.name);

            // Call functions based on marker names
            switch (marker.name)
            {
                case "ChorusStart":
                    // Invoke Unity function for ChorusStart
                    break;
                case "Drop":
                    // Invoke Unity function for Drop
                    break;
                case "VerseEnd":
                    // Invoke Unity function for VerseEnd
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
    }
}
