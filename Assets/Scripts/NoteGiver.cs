 using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class NoteGiver : MonoBehaviour
{
    [SerializeField] private Note notePrefab;
    [SerializeField] private GameObject redSlot;
    [SerializeField] private GameObject blueSlot;
    [SerializeField] private GameObject greenSlot;
    [SerializeField] private GameObject discSlot;
    
    public float noteSpawnInterval = 1f;
    private List<Note> notePool;
    private int poolSize = 208;

    public void SettingPool()
    {
        notePool = new List<Note>();
        for (int i = 0; i < poolSize; i++)
        {
            Note note = Instantiate(notePrefab);
            note.gameObject.SetActive(false);
            notePool.Add(note);
        }
    }
    
    private Note GetPooledNote()
    {
        foreach (Note note in notePool)
        {
            if (!note.gameObject.activeInHierarchy)
            {
                return note;
            }
        }
        return null;
    }
    
    public void SpawnNote(int colorID)
    {
        Note newNote = GetPooledNote();
        
            switch (colorID)
            {
                case 0:
                    newNote.ResetNote(redSlot.transform.position, "RED");
                    Debug.Log("Spawning red");
                    break;
                case 1:
                    newNote.ResetNote(blueSlot.transform.position, "BLUE");
                    Debug.Log("Spawning blue");
                    break;
                case 2:
                    newNote.ResetNote(greenSlot.transform.position, "GREEN");
                    Debug.Log("Spawning green");
                    break;
                case 3:
                    newNote.ResetNote(discSlot.transform.position, "DISC");
                    Debug.Log("Spawning disc");
                    break;
            }
    }
 }
