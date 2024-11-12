using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchNote : MonoBehaviour
{
    public Vector3 direction = Vector3.back; // Direction of movement
    public float speed = 5f; // Speed of movement
    public bool isOnTrigger;
    public string ID = "DISC";
    private int destroyAfter = 5; //Trigger this when the button har pressed correctly 
    
    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
