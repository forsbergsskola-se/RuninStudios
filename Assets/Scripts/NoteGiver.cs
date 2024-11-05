using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteGiver : MonoBehaviour

{
    [SerializeField] private Note notePrefab;
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
        Note note = Instantiate(notePrefab,transform.position,Quaternion.Euler(transform.rotation.x, transform.rotation.y+180, transform.rotation.z)); // Instantiate the Note prefab 
        note.ID = "BLUE";
        note.gameObject.SetActive(true);
    }
}
