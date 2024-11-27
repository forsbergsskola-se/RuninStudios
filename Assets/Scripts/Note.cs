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
    [SerializeField] private NoteReceiver noteReceiver;
    private SongManger2 songManger2;
    private ScoreManager scoreManager;

    private void Awake()
    {
        songManger2 = FindObjectOfType<SongManger2>();
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    void Update()
    {
        Move();
        CheckingDistance();
    }

    private void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void CheckingDistance()
    {
        if (!this.gameObject.activeSelf)
        {
            return;
        }
        
        float notepos = this.transform.position.z;
        float receiverpos = noteReceiver.transform.position.z;

        if (notepos > receiverpos - 1f && notepos < receiverpos + 1f)
        {
            noteReceiver.TriggerFlip(this, true);
        }
        else if (notepos < receiverpos - 2f)
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