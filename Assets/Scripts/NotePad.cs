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
            Debug.Log("Trigger ON!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Note")
        {
            TriggerFlip(other, false);
            Debug.Log("Trigger OFF!");
        }
    }

    private void TriggerFlip(Collider other, bool state)
    {
        other.GetComponent<Note>().isOnTrigger = state;
    }

}
