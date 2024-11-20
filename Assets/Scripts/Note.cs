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
    private float destroyAfter = 2f; //Trigger this when the button har pressed correctly 
    [SerializeField] private NoteReceiver noteReceiver;
    
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
        float noteTempZ = this.transform.position.z;
        float reciverTempZ = noteReceiver.transform.position.z;

        if (noteTempZ < reciverTempZ + 0.6f && noteTempZ > reciverTempZ - 0.8f)
        {
            noteReceiver.TriggerFlip(this, true);
        }
        else if (noteTempZ < reciverTempZ - 0.8f && this.gameObject.activeSelf)
        {
            noteReceiver.TriggerFlip(this, false);
            Debug.Log("Note Passed");
            FindObjectOfType<CvsConverter>().NoteMissed(); //Trigger the game over function in the Song Manager
            Debug.Log("NoteMissedFunctionCalled");
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