using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteReceiver : MonoBehaviour
{
    private GameObject redNote;
    private GameObject blueNote;
    private GameObject greenNote;

    [SerializeField] private ParticleSystem redSuccess;
    [SerializeField] private ParticleSystem blueSuccess;
    [SerializeField] private ParticleSystem greenSuccess;

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Note")
        {
            TriggerFlip(other, true);
            Debug.Log($"{other.GetComponent<Note>().ID}: Trigger ON!");
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Note")
        {
            TriggerFlip(other, false);
            Debug.Log($"{other.GetComponent<Note>().ID}: Trigger OFF!");
        }
    }

    private void TriggerFlip(Collider other, bool state)
    {
        other.GetComponent<Note>().isOnTrigger = state;
        if (state)
        {
            switch (other.GetComponent<Note>().ID)
            {
                case "RED":
                    redNote = other.gameObject;
                    break;
                case "BLUE":
                    blueNote = other.gameObject;
                    break;
                case "GREEN":
                    greenNote = other.gameObject;
                    break;
            }
        }
        else
        {
            switch(other.GetComponent<Note>().ID)
            {
                case "RED":
                    redNote = null;
                    break;
                case "BLUE":
                    blueNote = null;
                    break;
                case "GREEN":
                    greenNote = null;
                    break;
            }
        }
    }

    public void NotePadPressedRed()
    {
        try
        {
            if (redNote != null && redNote.GetComponent<Note>().isOnTrigger)
            {
                // Reset the particle system by re-enabling its GameObject
                redSuccess.gameObject.SetActive(false);
                redSuccess.gameObject.SetActive(true);

                redSuccess.Play();
                redNote.SetActive(false); // Disable the red note GameObject
                redNote = null;
            }
        }
        catch 
        { 
            Debug.Log("MISSED NOTE!"); 
        }
    }

    public void NotePadPressedBlue()
    {
        try
        {
            if (blueNote != null && blueNote.GetComponent<Note>().isOnTrigger)
            {
                blueSuccess.gameObject.SetActive(false);
                blueSuccess.gameObject.SetActive(true);

                blueSuccess.Play();
                blueNote.SetActive(false); // Disable the blue note GameObject
                blueNote = null;
            }
        }
        catch 
        { 
            Debug.Log("MISSED NOTE!"); 
        }
    }

    public void NotePadPressedGreen()
    {
        try
        {
            if (greenNote != null && greenNote.GetComponent<Note>().isOnTrigger)
            {
                greenSuccess.gameObject.SetActive(false);
                greenSuccess.gameObject.SetActive(true);

                greenSuccess.Play();
                greenNote.SetActive(false); // Disable the green note GameObject
                greenNote = null;
            }
        }
        catch 
        { 
            Debug.Log("MISSED NOTE!"); 
        }
    }
}
