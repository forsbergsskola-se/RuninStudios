 using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NoteGiver : MonoBehaviour

{
    [SerializeField] private GameObject redSlot;
    [SerializeField] private GameObject blueSlot;
    [SerializeField] private GameObject greenSlot;

    public void SendNote(Note note, string ID)
    {
        Quaternion slotRotation =
            Quaternion.Euler(transform.rotation.x, transform.rotation.y + 180, transform.rotation.z);
        switch (ID)
        {
            case "RED":
                Instantiate(note,redSlot.transform.position,slotRotation);
                note.gameObject.SetActive(true);
                break;
            case "BLUE":
                Instantiate(note, blueSlot.transform.position, slotRotation);
                note.gameObject.SetActive(true);
                break;
            case "GREEN":
                Instantiate(note, greenSlot.transform.position, slotRotation);
                note.gameObject.SetActive(true);
                break;
        }
        note.ID = ID;

    }
}
