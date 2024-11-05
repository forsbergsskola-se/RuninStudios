using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePad : MonoBehaviour
{
    private GameObject redNote;
    private GameObject blueNote;
    private GameObject greenNote;

    [SerializeField] private ParticleSystem redSuccess;
    [SerializeField] private ParticleSystem blueSuccess;
    [SerializeField] private ParticleSystem GreenSuccess;

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Note")
        {
            TriggerFlip(other, true);
            Debug.Log($"{other.GetComponent<Note>().ID}: Trigger ON!");
            
            switch(other.GetComponent<Note>().ID)
            {
                case "Red":
                    redNote = other.gameObject;
                    break;
                case "Blue":
                    blueNote = other.gameObject;
                    break;
                case "Green":
                    greenNote = other.gameObject;
                    break;
            }
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
                    redNote.GetComponent<Note>().isOnTrigger = state;
                    redNote = null;
                    break;
                case "BLUE":
                    blueNote.GetComponent<Note>().isOnTrigger = state;
                    blueNote = null;
                    break;
                case "GREEN":
                    greenNote.GetComponent<Note>().isOnTrigger = state;
                    greenNote = null;
                    break;
            }
        }
    }

    public void NotePadPressedRed()
    {
        if (redNote.GetComponent<Note>().isOnTrigger)
        {
            redNote.SetActive(false);  // Disable the red note GameObject
            redSuccess.Play();
        }
    } 
    public void NotePadPressedBlue()
    {
        if (blueNote.GetComponent<Note>().isOnTrigger)
        {
            blueNote.SetActive(false);  // Disable the blue note GameObject
            blueSuccess.Play();
        }
    }
    public void NotePadPressedGreen()
    {
        if (greenNote.GetComponent<Note>().isOnTrigger)
        {
            greenNote.SetActive(false);  // Disable the green note GameObject
            GreenSuccess.Play();
        }
    }

}
