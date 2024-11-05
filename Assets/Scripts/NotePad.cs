using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePad : MonoBehaviour
{
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
    }

    public void NotePadPressedRed()
    {
        Debug.Log("Red Button Pressed!");

    } 
    public void NotePadPressedBlue()
    {
        Debug.Log("Blue Button Pressed!");

    }
    public void NotePadPressedGreen()
    {
        Debug.Log("Green Button Pressed!");

    }

}
