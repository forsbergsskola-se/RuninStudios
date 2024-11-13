using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NoteReceiver : MonoBehaviour
{
    private GameObject redNote;
    private GameObject blueNote;
    private GameObject greenNote;
    private GameObject scratchNote;

    [SerializeField] private GameObject redTile;
    [SerializeField] private GameObject blueTile;
    [SerializeField] private GameObject greenTile;
    
    [SerializeField] private ParticleSystem redSuccess;
    [SerializeField] private ParticleSystem blueSuccess;
    [SerializeField] private ParticleSystem greenSuccess;
    [SerializeField] private ParticleSystem scratchSuccess;
    
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
                case "DISC":
                    scratchNote = other.gameObject;
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
                case "DISC":
                    scratchNote = null;
                    break;
            }
        }
    }

    public void NotePadPressedRed()
    {
        StartCoroutine(RedRoutine());
    }

    private IEnumerator RedRoutine()
    {
        var initialPosition = redTile.transform.position;
        var pressedPosition = initialPosition;
        pressedPosition.y -= 0.3f;
        redTile.transform.position = pressedPosition;
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
        yield return new WaitForSeconds(0.1f);
        redTile.transform.position = initialPosition;
    }

    public void NotePadPressedBlue()
    {
        StartCoroutine(BlueRoutine());
    }

    private IEnumerator BlueRoutine()
    {
        var initialPosition = blueTile.transform.position;
        var pressedPosition = initialPosition;
        pressedPosition.y -= 0.3f;
        blueTile.transform.position = pressedPosition;
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
        yield return new WaitForSeconds(0.1f);
        blueTile.transform.position = initialPosition;
    }

    public void NotePadPressedGreen()
    {
        StartCoroutine(GreenRoutine());
    }

    private IEnumerator GreenRoutine()
    {
        var initialPosition = greenTile.transform.position;
        var pressedPosition = initialPosition;
        pressedPosition.y -= 0.3f;
        greenTile.transform.position = pressedPosition;
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

        yield return new WaitForSeconds(0.1f);
        greenTile.transform.position = initialPosition;
    }

    public void NotePadScratch()
    {
        try
        {
            if (scratchNote != null && scratchNote.GetComponent<Note>().isOnTrigger)
            {
                scratchNote.gameObject.SetActive(false);
                scratchNote.gameObject.SetActive(true);

                scratchSuccess.Play();
                scratchNote.SetActive(false); // Disable the disc note GameObject
                scratchNote = null;
            }
        }
        catch 
        { 
            Debug.Log("MISSED NOTE!"); 
        }
    }
}
