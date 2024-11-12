 using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NoteGiver : MonoBehaviour

{
    [SerializeField] private Note notePrefab;
    [SerializeField] private GameObject redSlot;
    [SerializeField] private GameObject blueSlot;
    [SerializeField] private GameObject greenSlot;
    
    public float noteSpawnInterval = 1f;
    private float noteSpawnTimer = 0f;

    private void Update()
    {
        noteSpawnTimer += Time.deltaTime;
        if (noteSpawnTimer >= noteSpawnInterval)
        {
            noteSpawnTimer = 0f;
            SpawnNote();
        }
    }
    
    private void SpawnNote()
    {
        Quaternion slotRotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y + 180, transform.rotation.z);

        int randomIndex = Random.Range(0, 3);
        switch (randomIndex)
        {
            case 0:
                Note redNote = Instantiate(notePrefab,redSlot.transform.position,slotRotation); // Instantiate the Note prefab 
                redNote.ID = "RED";
                redNote.gameObject.SetActive(true);
                break; 
            case 1:
                Note blueNote = Instantiate(notePrefab,blueSlot.transform.position,slotRotation); // Instantiate the Note prefab 
                blueNote.ID = "BLUE";
                blueNote.gameObject.SetActive(true);
                break;
            case 2:
                Note greenNote = Instantiate(notePrefab,greenSlot.transform.position,slotRotation); // Instantiate the Note prefab 
                greenNote.ID = "GREEN";
                greenNote.gameObject.SetActive(true);
                break;
        }
        
        
    }
    /*[SerializeField] private GameObject redSlot;
    [SerializeField] private GameObject blueSlot;
    [SerializeField] private GameObject greenSlot;

    public void SendNote(Note note, string ID)
    {
        Quaternion slotRotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y + 180, transform.rotation.z);
        
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
    }*/
}