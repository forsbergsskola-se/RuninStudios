using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public Vector3 direction = Vector3.back; // Direction of movement
    public float speed = 5f; // Speed of movement
    public bool isOnTrigger;
    public string ID;
    private NoteReceiver noteReceiver;
    private SongManger2 songManger2;
    private ScoreManager scoreManager;

    private void Awake()
    {
        songManger2 = FindObjectOfType<SongManger2>();
        scoreManager = FindObjectOfType<ScoreManager>();
        noteReceiver = FindObjectOfType<NoteReceiver>();
    }

    void Update()
    {
        Move();
        CheckingDistance(this.transform.position.z, noteReceiver.transform.position.z);
    }

    private void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void CheckingDistance(float notepos, float receiverpos)
    {
        if (!this.gameObject.activeSelf) { return; }

        if (notepos > receiverpos - 2f && notepos < receiverpos + 2f)
        {
            noteReceiver.TriggerFlip(this, true);
        }
        if (notepos < receiverpos - 4f)
        {
            noteReceiver.TriggerFlip(this, false);
            Debug.Log("Note Passed");
            songManger2.NoteMissed(); //Trigger the game over function in the Song Manager
            Debug.Log("NoteMissedFunctionCalled");
            scoreManager.ResetMultiplier();
            Deactivate();
        }
    }
    
    public void TriggerHit()
    {
        CancelInvoke(); // Cancel the auto-deactivate
        Deactivate(); // Manually deactivate the note on hit
    }
    
    private void Deactivate()
    {
        gameObject.SetActive(false); // Recycle the note back into the pool
    }
    
    public void ResetNote(Vector3 startPosition, string colorID)
    {
        transform.position = startPosition;
        ID = colorID;
        gameObject.SetActive(true);
    }
}